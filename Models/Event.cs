namespace BirthCal.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Description { get; set; }
        
        public Person Person { get; set; }

        public Present Present { get; set; }

        public string Location { get; set; }

        public int? NumberOfVisitors { get; set; }

        public DateOnly? DateOfEvent { get; set; }

        public string? Comment { get; set; }
    }
}
