using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AnOrg.Models
{
    public class AdminDetails
    {
        public AdminDetails() { }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public long phone { get; set; }

    }
}
