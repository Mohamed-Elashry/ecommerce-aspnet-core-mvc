using E_Commerce.Data.Enums;

namespace E_Commerce.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public double Price { get; set; }
        [Display(Name = "Movie Image")]
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }
        public int CinemaId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
    }
}
