
namespace E_Commerce.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }

        // RelationShips
        public List<Movie>? Movies { get; set; }
    }
}
