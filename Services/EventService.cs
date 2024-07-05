using Microsoft.EntityFrameworkCore;
using BirthCal.Models;
using BirthCal.ViewModels;
using BirthCal.DTO;
using System.Diagnostics;


namespace BirthCal.Services
{
    public class EventService
    {
        private ApplicationDbContext _context;
        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EventsDropdownsViewModel> GetEventsDropdownsData()
        {
            var eventsDropdownsData = new EventsDropdownsViewModel()
            {
                People = await _context.People.OrderBy(person => person.LastName).ToListAsync(),
                Presents = await _context.Presents.OrderBy(present => present.Description).ToListAsync()
            };
            return eventsDropdownsData;
        }

        internal async Task CreateAsync(EventDTO eventDTO)
        {
            Event eventToInsert = await DtoToModel(eventDTO);
            await _context.Events.AddAsync(eventToInsert);
            await _context.SaveChangesAsync();
        }

        private async Task<Event> DtoToModel(EventDTO eventDTO)
        {
            return new Event()
            {
                Id = eventDTO.Id,
                Person = await _context.People.FirstOrDefaultAsync(person => person.Id == eventDTO.PersonId),
                Present = await _context.Presents.FirstOrDefaultAsync(present => present.Id == eventDTO.PresentId),
                DateOfEvent = eventDTO.DateOfEvent,
                Comment = eventDTO.Comment,
                Location = eventDTO.Location,
                NumberOfVisitors = eventDTO.NumberOfVisitors,
                Description = eventDTO.Description
            };
        }

        public async Task<IEnumerable<EventsViewModel>> GetAllVMsAsync()
        {
            List<Event> events = await _context.Events.Include(gr => gr.Person).Include(gr => gr.Present).ToListAsync();
            List<EventsViewModel> eventVMs = new List<EventsViewModel>();
            foreach (Event e in events)
            {
                eventVMs.Add(new EventsViewModel
                {
                    Id = e.Id,
                    PersonName = $"{e.Person.FirstName} {e.Person.LastName} ",
                    PresentName = e.Present.Description,
                    Location = e.Location,
                    NumberOfVisitors = e.NumberOfVisitors,
                    DateOfEvent = e.DateOfEvent,
                    Comment = e.Comment,
                    Description = e.Description
                    });
            }
            return eventVMs;
        }


        internal async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.Include(gr => gr.Person).Include(gr => gr.Present).FirstOrDefaultAsync(e => e.Id == id);
        }

        internal EventDTO ModelToDto(Event e)
        {
            return new EventDTO
            {
                 Id = e.Id,
                 PersonId = e.Person.Id,
                 PresentId = e.Present.Id,   
                 Location = e.Location,
                 NumberOfVisitors = e.NumberOfVisitors,
                 DateOfEvent = e.DateOfEvent,
                 Comment = e.Comment,  
                 Description = e.Description,
            };
        }

        internal async Task UpdateAsync(int id, EventDTO eventDTO)
        {
            Event updatedEvent = await DtoToModel(eventDTO);
            _context.Events.Update(updatedEvent);
            await _context.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id)
        {
            var eventToDelete = await _context.Events.FirstOrDefaultAsync(g => g.Id == id);
            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
