
namespace E_Commerce.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly IMoviesService _service;
        private readonly IStringLocalizer<MoviesController> _localizer;
        private readonly IGeneralService _generalService;

        public MoviesController(IMoviesService service, IStringLocalizer<MoviesController> localizer,IGeneralService generalService)
        {
            _service = service;
            _localizer = localizer;
            _generalService= generalService;
        } 
        public async Task<IActionResult> Index()
        {
            ViewBag.Welcome = string.Format(_localizer["Welcome"], "Mohamed");
            IEnumerable<Movie> movies = await _service.GetAllAsync(incl=> incl.Cinema);
                                  
            return View(movies);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var movie = await _service.GetMovieAsync(id);
            if(movie == null) return View("NotFound");
            return View(movie);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Actors = await _generalService.FillActors();
            ViewBag.Cinemas = await _generalService.FillCinemas();
            ViewBag.Producers = await _generalService.FillProducers();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieVM movieVM)
        {
            if (!ModelState.IsValid)
                return View(movieVM);

            await _service.AddAsync(movieVM);
            return RedirectToAction(nameof(Index));
        }
    }
}
