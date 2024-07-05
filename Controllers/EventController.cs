using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BirthCal.Services;
using BirthCal.DTO;
using Microsoft.AspNetCore.Authorization;

namespace BirthCal.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> CreateAsync()
        {
            await FillSelectsAsync();
            return View();
        }

        private async Task FillSelectsAsync()
        {
            var eventsDropdownsData = await _eventService.GetEventsDropdownsData();
            ViewBag.People = new SelectList(eventsDropdownsData.People, "Id", "LastName");
            ViewBag.Presents = new SelectList(eventsDropdownsData.Presents, "Id", "Description");
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventDTO eventDTO)
        {
            await _eventService.CreateAsync(eventDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var allEvents = await _eventService.GetAllVMsAsync();
            return View(allEvents);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var eventToEdit = await _eventService.GetByIdAsync(id);
            if (eventToEdit == null)
            {
                return View("NotFound");
            }
            var eventDto = _eventService.ModelToDto(eventToEdit);
            await FillSelectsAsync();
            return View(eventDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(int id, EventDTO eventDTO)
        {
            await _eventService.UpdateAsync(id, eventDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _eventService.DeleteAsync(id); return RedirectToAction("Index");
        }
    }
}
