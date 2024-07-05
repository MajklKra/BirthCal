using BirthCal.Models;

namespace BirthCal.ViewModels 
{
    public class EventsViewModel 
    {
        public int Id { get; set; }
  
        public string Description { get; set; }

        public string PersonName { get; set; }

        public string PresentName { get; set; }

        public string Location { get; set; }

        public int? NumberOfVisitors { get; set; }

        public DateOnly? DateOfEvent { get; set; }

        public string? Comment { get; set; }
    }
}
