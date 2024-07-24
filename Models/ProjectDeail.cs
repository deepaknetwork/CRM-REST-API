using Microsoft.VisualBasic;

namespace AnOrg.Models
{
    public class ProjectDeail
    {
        public long Id { get; set; }

        public string CustomerId { get; set; }

        public string State { get; set; }

        public List<string> Platforms { get; set; }

        public List<string> Technologies { get; set; }

        public List<string> Integrations { get; set; }

        public string Discription { get; set; }

        public List<string> Stakeholders { get; set; }

        public string ContactMedium { get; set; }

    }
}
