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

        public async Task<SelectList> FillActors(int id = 0)
        {
            var actors = await _context.Actors.Where(c=>c.Id == id || id == 0).OrderBy(c => c.FullName).ToListAsync();
            return new SelectList(actors, "Id", "FullName");

        }
        public async Task<SelectList> FillProducers(int id = 0)
        {
            var producers = await _context.Producers.Where(c => c.Id == id || id == 0).OrderBy(c => c.FullName).ToListAsync();
            return new SelectList(producers, "Id", "FullName");

        }
        public async Task<SelectList> FillCinemas(int id = 0)
        {
            var cinemas = await _context.Cinemas.Where(c => c.Id == id || id == 0).OrderBy(c => c.Name).ToListAsync();
            return new SelectList(cinemas, "Id", "Name");

        }
        public async Task<SelectList> FillMovies(int id = 0)
        {
            var movies = await _context.Movies.Where(c => c.Id == id || id == 0).OrderBy(c => c.Name).ToListAsync();
            return new SelectList(movies, "Id", "Name");

        }
    }
}
