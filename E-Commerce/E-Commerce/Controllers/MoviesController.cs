using System.Linq.Expressions;

namespace E_Commerce.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<MoviesController> _localizer;
        private readonly IGeneralService _generalService;

        public MoviesController(IUnitOfWork unitOfWork, IStringLocalizer<MoviesController> localizer,IGeneralService generalService)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _generalService= generalService;
        } 
        public async Task<IActionResult> Index()
        {
            ViewBag.Welcome = string.Format(_localizer["Welcome"], "Mohamed");
            IEnumerable<Movie> movies = await _unitOfWork.Movies.GetAllAsync(incl=> incl.Cinema);
                                  
            return View(movies);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var movie = await _unitOfWork.Movies.GetMovieAsync(id);
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

            await _unitOfWork.Movies.AddAsync(movieVM);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var _movie = await _unitOfWork.Movies.GetMovieAsync(id);
            if (_movie == null) return View("NotFound");
            MovieVM movieVM = new MovieVM()
            {
                MovieId = _movie.Id,
                CinemaId = _movie.CinemaId,
                Name = _movie.Name,
                Price = _movie.Price,
                Description = _movie.Description,
                StartDate = _movie.StartDate,
                EndDate = _movie.EndDate,
                ImageURL = _movie.ImageURL,
                ProducerId = _movie.ProducerId,
                MovieCategory = _movie.MovieCategory,
                ActorIds = _movie.Actors_Movies.Select(s => s.ActorId).ToList()
            };
            ViewBag.Actors = await _generalService.FillActors();
            ViewBag.Cinemas = await _generalService.FillCinemas();
            ViewBag.Producers = await _generalService.FillProducers();
            return View(movieVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,MovieVM movieVM)
        {
            if (!ModelState.IsValid)
                return View(movieVM);
            if (movieVM.MovieId != id) return View("NotFound");

            await _unitOfWork.Movies.UpdateAsync(id,movieVM);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Filter(string searchString)
        {
            Expression<Func<Movie, bool>> expressionFilter = (
                c => c.Name.Contains(searchString) ||c.Description.Contains(searchString));
            Expression<Func<Movie, object>>[] includeProperties =new Expression<Func<Movie, object>>[] { (incl => incl.Cinema) };
            IEnumerable<Movie> _result = await _unitOfWork.Movies.FilterAsync(expressionFilter, includeProperties);
            if(!_result.Any())
                _result = await _unitOfWork.Movies.GetAllAsync(incl => incl.Cinema);
            return View("Index",_result);
        }
    }
}
