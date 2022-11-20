using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ActorsController : Controller
    {
        private readonly AppDbContext _context;
        public ActorsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Actor> actors = new List<Actor>();
            actors =await _context.Actors.ToListAsync();
            return View(actors);
        }
    }
}
