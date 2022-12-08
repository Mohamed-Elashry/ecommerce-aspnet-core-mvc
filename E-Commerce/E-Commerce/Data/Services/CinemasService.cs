
namespace E_Commerce.Data.Services
{
    public class CinemasService: EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context):base(context)
        {
        }
    }
}
