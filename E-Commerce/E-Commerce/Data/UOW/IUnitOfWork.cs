namespace E_Commerce.Data.UOW
{
    public interface IUnitOfWork:IDisposable
    {
        IEntityBaseRepository<Actor> Actors { get; }
        IEntityBaseRepository<Cinema> Cinemas { get; }
        IEntityBaseRepository<Producer> Producers { get; }
        IMoviesService Movies { get; }
        IGeneralService GeneralServices { get; }

        Task<int> Complete();
    }
}
