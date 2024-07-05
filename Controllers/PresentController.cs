using BirthCal.DTO;
using BirthCal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BirthCal.Controllers
{
    [Authorize]
    public class PresentController : Controller
    {
        private PresentService _service;

        public PresentController(PresentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allPresents = await _service.GetAllAsync();
            return View(allPresents);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PresentDTO newPresent)
        {
            await _service.CreateAsync(newPresent);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var presentToEdit = await _service.GetByIdAsync(id);

            if (presentToEdit == null)
            {
                return View("NotFound");
            }
            return View(presentToEdit);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(int id, PresentDTO present)
        {
            await _service.UpdateAsync(id, present);
            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var presentToDelete = await _service.GetByIdAsync(id);

            if (presentToDelete == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Birthday()
        {
            var allPresents = await _service.GetAllAsync();
            return View(allPresents);
        }
    }
}
