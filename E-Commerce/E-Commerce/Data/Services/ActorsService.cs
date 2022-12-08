namespace E_Commerce.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext dbContext): base(dbContext) { }
     
    }
}
