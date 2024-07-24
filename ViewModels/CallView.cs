using Microsoft.VisualBasic;
using System.Runtime.Serialization;

namespace AnOrg.ViewModels
{
    public class CallView
    {

        public string Catogory { get; set; }

        public string CustomerId { get; set; }

        public long ProjectId { get; set; }

        public long Phone { get; set; }

        public DateTime time { get; set; }
    }
}
