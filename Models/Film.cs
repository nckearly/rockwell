using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Rockwell.Models
{
    public partial class Film
    {
        public Film()
        {
            FilmActor = new HashSet<FilmActor>();
            FilmReview = new HashSet<FilmReview>();
            Merchandise = new HashSet<Merchandise>();
        }
        [Required(ErrorMessage ="Input Required")]
        public int FilmPk { get; set; }
        [Required(ErrorMessage = "Input Required")]
        public string MovieTitle { get; set; }
        [Required(ErrorMessage = "Input Required")]
        public string PitchText { get; set; }
        [Required(ErrorMessage = "Input Required")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public long? AmountBudgeted { get; set; }
        [Required(ErrorMessage = "Input Required")]
        public int? RatingFk { get; set; }
        [Required(ErrorMessage = "Input Required")]
        public string Summary { get; set; }
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Input Required")]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:d}")]
        public DateTime? DateInTheaters { get; set; }

        public virtual FilmRating RatingFkNavigation { get; set; }
        public virtual ICollection<FilmActor> FilmActor { get; set; }
        public virtual ICollection<FilmReview> FilmReview { get; set; }
        public virtual ICollection<Merchandise> Merchandise { get; set; }
    }
}
