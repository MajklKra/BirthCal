using BirthCal.DTO;
using BirthCal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BirthCal.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private PersonService _service;

        public PersonController(PersonService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allPeople = await _service.GetAllAsync();
            return View(allPeople);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonDTO newPerson)
        {
            await _service.CreateAsync(newPerson);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var personToEdit = await _service.GetByIdAsync(id);

            if (personToEdit == null)
            {
                return View("NotFound");
            }
            return View(personToEdit);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update (int id, PersonDTO person)
        {
            await _service.UpdateAsync(id, person);
            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var personToDelete = await _service.GetByIdAsync(id);

            if (personToDelete == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Birthday()
        {
            var allPeople = await _service.GetAllAsync();
            return View(allPeople);
        }

       
    }
}
