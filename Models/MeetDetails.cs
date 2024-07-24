namespace AnOrg.Models
{
    public class MeetDetails
    {
        public long Id { get; set; }

        public string CustomerId { get; set; }

        public long ProjectId { get; set; }

        public string link { get; set; }

        public DateTime time { get; set; }

        public string status { get; set; }
    }
}
