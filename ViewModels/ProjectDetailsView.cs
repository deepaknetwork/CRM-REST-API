namespace AnOrg.ViewModels
{
    public class ProjectDetailsView
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Domain { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public string State { get; set; }

    }
}
