using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AnOrg.Models
{
    public class CustomerDetails
    {
        public string Id{ get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Password { get; set; }
        public bool IsClient { get; set; }
    }
}
