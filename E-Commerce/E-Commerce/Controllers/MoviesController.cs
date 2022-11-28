
namespace E_Commerce.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IStringLocalizer<MoviesController> _localizer;
        public MoviesController(AppDbContext context, IStringLocalizer<MoviesController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Welcome = string.Format(_localizer["Welcome"], "Mohamed");
            List<Movie> movies = new List<Movie>();
            movies = await _context.Movies
                                   .Include(c=>c.Cinema)
                                   .Include(p=>p.Producer)
                                   .ToListAsync();
            return View(movies);
        }
    }
}
