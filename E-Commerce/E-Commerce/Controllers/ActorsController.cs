﻿using E_Commerce.Models;
using Microsoft.Extensions.Localization;

namespace E_Commerce.Controllers
{
    public class ActorsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ActorsController> _localizer;
        public ActorsController(IUnitOfWork unitOfWork, IStringLocalizer<ActorsController> localizer)
        {
            _unitOfWork= unitOfWork;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Welcome = _localizer["Welcome"];
            IEnumerable<Actor> actors = await _unitOfWork.Actors.GetAllAsync();
            return View(actors);
        }
        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);

            
            var result = await _unitOfWork.Actors.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _unitOfWork.Actors.GetByIdAsync(id);
            if (result.Id > 0)
                return View(result);
            else
                return View("NotFound");
        }
        public async Task<IActionResult> Update(int id)
        {
            var actor = await _unitOfWork.Actors.GetByIdAsync(id);
            if (actor.Id > 0)
            {
                return View(actor);
            }
            else
                return View("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);
            if (id == actor.Id)
            {
                await _unitOfWork.Actors.UpdateAsync(id, actor);
                return RedirectToAction(nameof(Index));
            }
            else
                return View("NotFound");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _unitOfWork.Actors.GetByIdAsync(id);
            if (id == actor.Id)
            {
                return View(actor);
            }
            else
                return View("NotFound");
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var result = await _unitOfWork.Actors.GetByIdAsync(id);
            if (id == result.Id)
            {
                await _unitOfWork.Actors.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else
                return View("NotFound");
        }

    }
}
