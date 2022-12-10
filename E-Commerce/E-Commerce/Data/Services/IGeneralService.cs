using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Data.Services
{
    public interface IGeneralService
    {
        Task<SelectList> FillActors(int id = 0);
        Task<SelectList> FillProducers(int id = 0);
        Task<SelectList> FillCinemas(int id = 0);
        Task<SelectList> FillMovies(int id = 0);
        
    }
}
