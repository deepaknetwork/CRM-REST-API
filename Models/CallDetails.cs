using Microsoft.VisualBasic;
using System.Runtime.Serialization;

namespace AnOrg.Models
{
    public class CallDetails
    {
        public long Id { get; set; }

        public string Catogory { get; set; }

        public string CustomerId { get; set; }

        public long ProjectId { get; set; }

        public long Phone { get; set; }

        public DateTime time {  get; set; }

        public string status { get; set; }
    }
}
