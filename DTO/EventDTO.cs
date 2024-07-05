using BirthCal.Models;

namespace BirthCal.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int PersonId { get; set; }

        public int PresentId { get; set; }

        public string Location { get; set; }

        public int? NumberOfVisitors { get; set; }

        public DateOnly? DateOfEvent { get; set; }

        public string? Comment { get; set; }
    }
}
