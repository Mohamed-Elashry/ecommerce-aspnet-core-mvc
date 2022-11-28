namespace E_Commerce.Data.Services
{
    public interface IActorsService
    {
        Task<IEnumerable<Actor>> GetActors();
        Task<Actor> GetActor(int Id);
        Task<bool> Add(Actor actor);
        Task<bool> Update(int Id, Actor NewActor);
        Task<bool> Delete(int Id);
    }
}
