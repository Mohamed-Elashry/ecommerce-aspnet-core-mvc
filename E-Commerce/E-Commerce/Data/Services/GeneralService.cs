using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Data.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly AppDbContext _context;
        public GeneralService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SelectList> FillActors()
        {
            var actors = await _context.Actors.OrderBy(c => c.FullName).ToListAsync();
            return new SelectList(actors, "Id", "FullName");

        }
        public async Task<SelectList> FillProducers()
        {
            var producers = await _context.Producers.OrderBy(c => c.FullName).ToListAsync();
            return new SelectList(producers, "Id", "FullName");

        }
        public async Task<SelectList> FillCinemas()
        {
            var cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync();
            return new SelectList(cinemas, "Id", "Name");

        }
        public async Task<SelectList> FillMovies()
        {
            var movies = await _context.Movies.OrderBy(c => c.Name).ToListAsync();
            return new SelectList(movies, "Id", "Name");

        }
    }
}
