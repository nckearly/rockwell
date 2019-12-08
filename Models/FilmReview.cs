using System;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations; 
namespace Rockwell.Models
{
    public partial class FilmReview
    {
        public int ReviewPk { get; set; }

        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        [Display(Name = "Summary")]
        [Required(ErrorMessage ="Input Required")]
        public string ReviewSummary { get; set; }

        [Display(Name = "Rating")]
        [Required(ErrorMessage = "Input Required")]
        [Range(0,5, ErrorMessage = "Enter a rating between 0 and 5")]
        public short ReviewRating { get; set; }

       
        public int FilmFk { get; set; }

        
        public int ContactFk { get; set; }
        [Display(Name = "User")]
        public virtual Contact ContactFkNavigation { get; set; }

        [Display(Name = "Film")]
        public virtual Film FilmFkNavigation { get; set; }
    }
}
