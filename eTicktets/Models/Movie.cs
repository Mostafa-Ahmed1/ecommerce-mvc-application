﻿using eTicktets.Data.Base;
using eTicktets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTicktets.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        // [Display(Name = "Movie Name")]
        public string Name { get; set; }

        // [Display(Name = "Movie Description")]
        public string Description { get; set; }

        // [Display(Name = "Movie Price")]
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        // Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }

        // Cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        // Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
    }
}
