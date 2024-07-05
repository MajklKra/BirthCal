using BirthCal.Models;

namespace BirthCal.ViewModels {
    public class EventsDropdownsViewModel {
        public IEnumerable<Person> People { get; set; }
        public IEnumerable<Present> Presents { get; set; }
        public EventsDropdownsViewModel() {
            People = new List<Person>();
            Presents = new List<Present>();
        }
    }
}
