using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AnOrg.Models
{
    public class Organization
    { 
        public long Id { get; set; }
        public long ProjectDealscnt {  get; set; }
        public long Projectcnt { get; set; }
        public long Callcnt { get; set; }
        public long Meetcnt { get; set; }



    }
}
