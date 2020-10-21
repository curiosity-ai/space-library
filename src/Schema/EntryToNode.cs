using System;

namespace SpaceLibrary
{
    public static class EntryToNode
    {
        //Helper function to map the JSON schema into our Node schema for a Library Entry
        public static LibraryEntry Parse(string entryID, Json.CitationSchema citation)
        {
            var entry = new LibraryEntry
            {
                ID                    = entryID,
                EntryType             = citation.stiType.Replace("_", " ").ToTitleCase(),
                Title                 = citation.title,
                Abstract              = citation._abstract ?? "",
                Distribution          = citation.distribution,
                ExportControlCategory = (citation.exportControl?.category ?? 0).ToString(),
                Status                = citation.status ?? "",
                ExportControlITAR     = citation.exportControl?.itar is object && (citation.exportControl.itar == "NO" ? false : true),
                ExportControlEAR      = citation.exportControl?.ear is object && (citation.exportControl.ear == "NO" ? false : true),
                OnlyAbstract          = citation.onlyAbstract,
                IsLessonsLearned      = citation.isLessonsLearned,
                SensitiveInformation  = citation.sensitiveInformation,
                DistributionDate      = citation.distributionDate.Year > 1950 ? citation.distributionDate : DateTime.UnixEpoch,
                SubmittedDate         = citation.submittedDate.Year > 1950 ? citation.submittedDate : DateTime.UnixEpoch,
                Modified              = citation.modified.Year > 1950 ? citation.modified : DateTime.UnixEpoch,
                Created               = citation.created.Year > 1950 ? citation.created : DateTime.UnixEpoch
            };

            return entry;
        }
    }
}
