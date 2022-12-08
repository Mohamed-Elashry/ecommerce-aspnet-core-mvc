using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Data.Services
{
    public interface IGeneralService
    {
        Task<SelectList> FillActors();
        Task<SelectList> FillProducers();
        Task<SelectList> FillCinemas();
        Task<SelectList> FillMovies();
        
    }
}
