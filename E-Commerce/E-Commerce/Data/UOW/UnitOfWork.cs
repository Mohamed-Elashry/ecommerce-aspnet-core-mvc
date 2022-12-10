namespace E_Commerce.Data.UOW
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IEntityBaseRepository<Actor> Actors { get; set; }
        public IEntityBaseRepository<Cinema> Cinemas { get; set; }
        public IEntityBaseRepository<Producer> Producers { get; set; }
        public IMoviesService Movies { get; set; }
        public IGeneralService GeneralServices { get; set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Actors = new EntityBaseRepository<Actor>(_context);
            Cinemas = new EntityBaseRepository<Cinema>(_context);
            Producers = new EntityBaseRepository<Producer>(_context);
            Movies = new MoviesService(_context);
            GeneralServices = new GeneralService(_context);
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
