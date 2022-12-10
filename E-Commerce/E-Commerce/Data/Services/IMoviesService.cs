namespace E_Commerce.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie> 
    {
        Task<Movie> GetMovieAsync(int id);
        Task AddAsync(MovieVM data);
        Task UpdateAsync(int id, MovieVM data);
    }
}
