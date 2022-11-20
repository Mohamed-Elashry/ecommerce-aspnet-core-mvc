using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Movie> movies = new List<Movie>();
            movies = await _context.Movies
                                   .Include(c=>c.Cinema)
                                   .Include(p=>p.Producer)
                                   .ToListAsync();
            return View(movies);
        }
    }
}
