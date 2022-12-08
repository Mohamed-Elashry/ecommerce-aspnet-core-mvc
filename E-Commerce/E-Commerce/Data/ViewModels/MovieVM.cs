
using E_Commerce.Data.Enums;

namespace E_Commerce.Models
{
    public class MovieVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        public List<int> ActorIds { get; set; }
        public int CinemaId { get; set; }
        public int ProducerId { get; set; }
    }
}
