namespace E_Commerce.Data.Services
{
    public class MoviesService:EntityBaseRepository<Movie>,IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context):base(context)
        {
            _context= context;
        }

        public async Task AddAsync(MovieVM data)
        {
            var newMovie = new Movie()
            {
                CinemaId = data.CinemaId,
                Name = data.Name,
                Price = data.Price,
                Description = data.Description,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                ImageURL = data.ImageURL,
                ProducerId = data.ProducerId,
            };
            await _context.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            if (data.ActorIds.Count() > 0)
            {
                List<Actor_Movie> actors_Movie = new List<Actor_Movie>();
                foreach (var actorId in data.ActorIds)
                {
                    actors_Movie.Add(new Actor_Movie()
                    {
                        MovieId = newMovie.Id,
                        ActorId = actorId,
                    });
                }
                if(actors_Movie.Count() > 0)
                {
                    await _context.AddRangeAsync(actors_Movie);
                    await _context.SaveChangesAsync();
                }

            }
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            var movie = await _context.Movies
                                      .Include(c => c.Cinema)
                                      .Include(p => p.Producer)
                                      .Include(am => am.Actors_Movies)
                                      .ThenInclude(a => a.Actor)
                                      .FirstOrDefaultAsync(s => s.Id == id);
            return movie;
        }
    }
}
