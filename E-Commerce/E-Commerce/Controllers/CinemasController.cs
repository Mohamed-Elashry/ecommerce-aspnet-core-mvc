using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CinemasController : Controller
    {
        private readonly AppDbContext _context;
        public CinemasController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Cinema> cinemas = new List<Cinema>();
            cinemas = await _context.Cinemas.ToListAsync();
            return View(cinemas);
        }
    }
}
