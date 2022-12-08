using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Cinema> cinemas = await _service.GetAllAsync();
            return View(cinemas);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var cinema = await _service.GetByIdAsync(Id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Cinema cinema)
        {
            if (id == cinema.Id)
            {
                await _service.UpdateAsync(id, cinema);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("NotFound");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinema = _service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
