using Curiosity.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace SpaceLibrary
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            if(args is null || args.Length != 2)
            {
                PrintHelp();
                return;
            }

            var (server, token) = (args[0], args[1]);

            var dataFolder = Path.GetFullPath("data");
            
            Directory.CreateDirectory(dataFolder);

            Console.Write("Downloading NASA Library, this might take a while...");

            await DownloadFilesAsync(dataFolder);

            using (var graph = Graph.Connect(server, token, "NASA Library"))
            {
                await CreateSchemaAsync(graph);
                
                await UploadDataAsync(dataFolder, graph);
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Missing server URL and API token.");
        }

        private static async Task UploadDataAsync(string dataFolder, Graph graph)
        {
            var tasks = new List<Task>();

            foreach (var yearFolder in Directory.GetDirectories(dataFolder))
            {
                if (int.TryParse(new DirectoryInfo(yearFolder).Name, out var year))
                {
                    tasks.Add(ProcessYearAsync(graph, yearFolder, year));
                }
            }
            await Task.WhenAll(tasks);
        }

        private static async Task DownloadFilesAsync(string dataFolder)
        {
            var bsc = new BlobServiceClient(new Uri("http://data.curiosity.ai/"));
            var cc   = bsc.GetBlobContainerClient("space-library");

            await foreach (var blob in cc.GetBlobsAsync())
            {
                var client = cc.GetBlobClient(blob.Name);

                var downloadFilePath = Path.Combine(dataFolder, blob.Name);

                if (!File.Exists(downloadFilePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(downloadFilePath));

                    Console.Write($"Downloading {blob.Name} [{blob.Properties.ContentLength:n0} bytes] ");

                    await client.DownloadToAsync(downloadFilePath);

                    Console.WriteLine("...done !");
                }
            }
        }

        private static async Task ProcessYearAsync(Graph graph, string yearFolder, int year)
        {
            foreach (var entryFolder in Directory.GetDirectories(yearFolder))
            {
                try
                {
                    var entryID = new DirectoryInfo(entryFolder).Name;
                    
                    Console.WriteLine($"Ingesting {entryID}");

                    var jsonFile = Path.Combine(entryFolder, $"{entryID}.json");
                    var attachmentsFolder = Path.Combine(entryFolder, "attachments");

                    if (File.Exists(jsonFile))
                    {
                        var entryData = JsonConvert.DeserializeObject<Json.CitationSchema>(await File.ReadAllTextAsync(jsonFile));

                        if (entryData is object)
                        {
                            var entry = EntryToNode.Parse(entryID, entryData);
                            var entryNode = graph.AddOrUpdate(entry);

                            var files = new List<Node>();

                            if (entryData.downloads is object)
                            {
                                foreach (var file in entryData.downloads)
                                {
                                    var filePath = Path.Combine(attachmentsFolder, file.name);
                                    if (File.Exists(filePath))
                                    {
                                        Console.WriteLine($"Uploading {file.name} to {entryID}");

                                        using var s = File.OpenRead(filePath);
                                        var fileNameOnGraph = $"/NASA Library/{year}/{entryID}/{file.name}";
                                        var fileNode = await graph.UploadFileAsync(s, fileNameOnGraph, "NASA Library");

                                        graph.Link(entryNode, fileNode, Edges._HasAttachment, Edges._AttachmentOf);

                                        files.Add(fileNode);

                                        var metadata = new Dictionary<string, string>();
                                        metadata["Title"] = entryData.title;

                                        graph.Update(fileNode, new Dictionary<string, object>() { ["Metadata"] = metadata });
                                    }
                                }
                            }

                            if (entryData.authorAffiliations is object)
                            {
                                foreach (var author in entryData.authorAffiliations)
                                {
                                    if (string.IsNullOrWhiteSpace(author.meta.author.name)) continue;

                                    var authorNode = graph.AddOrUpdate(new Author() { Name = author.meta.author.name });

                                    if (!string.IsNullOrWhiteSpace(author.meta.organization.name))
                                    {
                                        var orgNode = graph.AddOrUpdate(new Organization() { Name = author.meta.organization.name, Location = author.meta.organization.location });

                                        graph.Link(authorNode, orgNode, Edges.AssociatedTo, Edges.HasMember);
                                    }

                                    files.ForEach(fileNode => graph.Link(fileNode, authorNode, author.primary ? Edges.HasPrimaryAuthor : Edges.HasAuthor, Edges.AuthorOf));
                                }
                            }

                            if (entryData.subjectCategories is object)
                            {
                                foreach (var cat in entryData.subjectCategories)
                                {
                                    if (string.IsNullOrWhiteSpace(cat)) continue;

                                    var catUID = graph.AddOrUpdate(new Category() { Name = cat.ToTitleCase() });

                                    files.ForEach(fileUID => graph.Link(fileUID, catUID, Edges.HasCategory, Edges.CategoryOf));
                                }
                            }

                            if (entryData.related is object)
                            {
                                foreach (var rel in entryData.related)
                                {
                                    if (string.IsNullOrWhiteSpace(rel.accessionNumber)) continue;

                                    var relUID = graph.AddOrUpdate(new AccessionEntry() { Number = rel.accessionNumber, Title = rel.title, EntryType = rel.type.Replace("_", " ").ToTitleCase() });

                                    files.ForEach(fileUID =>
                                    {
                                        graph.Link(fileUID, relUID, Edges.HasRelated, Edges.RelatedTo);
                                    });
                                }
                            }
                        }
                    }
                }
                catch(Exception E)
                {
                    Console.WriteLine($"Error ingesting {entryFolder}, skipping.\n{E}");
                }
            }
        }

        private static async Task CreateSchemaAsync(Graph graph)
        {
            await graph.CreateNodeSchemaAsync<LibraryEntry>();
            await graph.CreateNodeSchemaAsync<DocumentType>();

            await graph.CreateNodeSchemaAsync<Author>();
            await graph.CreateNodeSchemaAsync<Organization>();
            await graph.CreateNodeSchemaAsync<Category>();
            await graph.CreateNodeSchemaAsync<AccessionEntry>();

            await graph.CreateEdgeSchemaAsync(Edges.GetAll());
        }
    }
}
