namespace E_Commerce.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _dbContext;
        public ActorsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Actor actor)
        {
            if (actor != null)
            {
               await _dbContext.Actors.AddAsync(actor);
               var result = await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int Id)
        {
            var actor = await _dbContext.Actors.FindAsync(Id);
            if (actor != null)
            {
                _dbContext.Actors.Remove(actor);
                var result = await _dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<Actor> GetActor(int Id)
        {
            var result = await _dbContext.Actors.FindAsync(Id);
            if(result != null)
                return result;
            else
                return new Actor();
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            return await _dbContext.Actors.ToListAsync();
        }

        public async Task<bool> Update(int Id, Actor NewActor)
        {
            var actor = await _dbContext.Actors.FindAsync(Id);
            if (actor != null)
            {
                actor.ProfilePictureUrl = NewActor.ProfilePictureUrl;
                actor.FullName = NewActor.FullName;
                actor.Bio = NewActor.Bio;
                _dbContext.Actors.Update(actor);    
                var result = await _dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
