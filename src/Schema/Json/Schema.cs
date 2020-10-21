using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceLibrary.Json
{
    public class CitationSchema
    {
        public string[] subjectCategories { get; set; }
        public string[] keywords { get; set; }
        public Exportcontrol exportControl { get; set; }
        public Otherdetails otherDetails { get; set; }
        public DateTime distributionDate { get; set; }
        public string[] otherReportNumbers { get; set; }
        public Fundingnumber[] fundingNumbers { get; set; }
        public string title { get; set; }
        public string stiType { get; set; }
        public string distribution { get; set; }
        public DateTime submittedDate { get; set; }
        public Authoraffiliation[] authorAffiliations { get; set; }
        public string technicalReviewType { get; set; }
        public DateTime modified { get; set; }
        public long id { get; set; }
        public DateTime created { get; set; }
        public Center center { get; set; }
        public bool onlyAbstract { get; set; }
        public int sensitiveInformation { get; set; }
        public string _abstract { get; set; }
        public bool isLessonsLearned { get; set; }
        public string disseminated { get; set; }
        public Publication[] publications { get; set; }
        public string status { get; set; }
        public Related[] related { get; set; }
        public Download[] downloads { get; set; }
        public bool downloadsAvailable { get; set; }
    }

    public class Related
    {
        public string accessionNumber { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }
    public class Exportcontrol
    {
        public string itar { get; set; }
        public int category { get; set; }
        public string ear { get; set; }
    }

    public class Otherdetails
    {
    }

    public class Center
    {
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
    }

    public class Fundingnumber
    {
        public string number { get; set; }
        public string type { get; set; }
    }

    public class Authoraffiliation
    {
        public string id { get; set; }
        public int sequence { get; set; }
        public bool primary { get; set; }
        public long submissionId { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public Author author { get; set; }
        public Organization organization { get; set; }
    }

    public class Author
    {
        public string name { get; set; }
    }

    public class Organization
    {
        public string name { get; set; }
        public string location { get; set; }
    }

    public class Publication
    {
        public string id { get; set; }
        public DateTime publicationDate { get; set; }
        public long submissionId { get; set; }
    }

    public class Download
    {
        public bool draft { get; set; }
        public string mimetype { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Links links { get; set; }
    }

    public class Links
    {
        public string original { get; set; }
        public string pdf { get; set; }
        public string fulltext { get; set; }
    }

}
