
namespace E_Commerce.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }  
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // RelationShips
        public List<Movie>? Movies { get; set; }
      
    }
}
