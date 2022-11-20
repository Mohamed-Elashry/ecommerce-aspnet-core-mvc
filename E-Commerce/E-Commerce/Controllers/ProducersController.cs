using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext _context;
        public ProducersController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Producer> producers = new List<Producer>();
            producers =await _context.Producers.ToListAsync();
            return View(producers);
        }
    }
}
