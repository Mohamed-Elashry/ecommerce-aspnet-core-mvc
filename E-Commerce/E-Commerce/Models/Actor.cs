﻿
namespace E_Commerce.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        //[Display(Name = "Profile Picture ")]
        public string ProfilePictureUrl { get; set; }
        //[Display(Name = "Full Name")]
        public string FullName { get; set; }
        //[Display(Name = "Biography")]
        public string Bio { get; set; }

        //Relationships
        public List<Actor_Movie>? Actors_Movies { get; set; }
    }
}
