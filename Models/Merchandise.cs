using System;
using System.Collections.Generic;

//add for validation
using System.ComponentModel.DataAnnotations;
namespace Rockwell.Models
{
    public partial class Merchandise
    {
        public int MerchandisePk { get; set; }
        public int FilmFk { get; set; }
        [Required(ErrorMessage ="Merchandise name is required")]
        public string MerchandiseName { get; set; }
        public string MerchandiseDescription { get; set; }
        [Range(2,500,ErrorMessage ="Price must be 2-500")]
        public decimal MerchandisePrice { get; set; }
        public string ImageNameSmall { get; set; }
        public string ImageNameLarge { get; set; }

        public virtual Film FilmFkNavigation { get; set; }
    }
}
