using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProducersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Producer> producers = await _unitOfWork.Producers.GetAllAsync();
            return View(producers);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var producer = await _unitOfWork.Producers.GetByIdAsync(Id);
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
            await _unitOfWork.Producers.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var producers = await _unitOfWork.Producers.GetByIdAsync(id);
            if (producers == null) return View("NotFound");
            return View(producers);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Producer producer)
        {
            if (id ==producer.Id)
            {
                await _unitOfWork.Producers.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("NotFound");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producer =await _unitOfWork.Producers.GetByIdAsync(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var producer = _unitOfWork.Producers.GetByIdAsync(id);
            if (producer == null) return View("NotFound");
            await _unitOfWork.Producers.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
