namespace E_Commerce.Data.Services
{
    public class ProducersService:EntityBaseRepository<Producer>,IProducersService
    {
        public ProducersService(AppDbContext context):base(context) { }
       
    }
}
