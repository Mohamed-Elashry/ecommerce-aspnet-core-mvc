﻿using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service= service;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Producer> producers = await _service.GetAllAsync();
            return View(producers);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var producer = await _service.GetByIdAsync(Id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {
            if (!ModelState.IsValid)
                return View(producer);
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var producers = await _service.GetByIdAsync(id);
            if (producers == null) return View("NotFound");
            return View(producers);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Producer producer)
        {
            if (id ==producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("NotFound");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producer =await _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var producer = _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
