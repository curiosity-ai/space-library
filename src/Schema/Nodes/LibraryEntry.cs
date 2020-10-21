using Curiosity.Library;
using System;

namespace SpaceLibrary
{
    [Node]
    public class LibraryEntry 
    { 
        [Key]       public string  ID                       { get; set; }
        [Property]  public string  Title                    { get; set; }
        [Property]  public string  EntryType                { get; set; }
        [Property]  public string  Abstract                 { get; set; }
        [Property]  public string  Distribution             { get; set; }
        [Property]  public string  ExportControlCategory    { get; set; }
        [Property]  public string  Status                   { get; set; }
        [Property]  public bool    ExportControlITAR        { get; set; }
        [Property]  public bool    ExportControlEAR         { get; set; }
        [Property]  public bool    OnlyAbstract             { get; set; }
        [Property]  public bool    IsLessonsLearned         { get; set; }
        [Property]  public int     SensitiveInformation     { get; set; }
        [Property]  public DateTime DistributionDate        { get; set; }
        [Property]  public DateTime SubmittedDate           { get; set; }
        [Property]  public DateTime Modified                { get; set; }
        [Timestamp] public DateTime Created                 { get; set; }
    }
}
