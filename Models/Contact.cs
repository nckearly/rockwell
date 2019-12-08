using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
namespace Rockwell.Models
{
    public partial class Contact
    {
        public Contact()
        {
            FilmReview = new HashSet<FilmReview>();
        }

        public int ContactPk { get; set; }
        [Required(ErrorMessage ="Input Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Input Required")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Input Required")]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage="Alpha-numeric characters only")]
        public string UserLogin { get; set; }
        [Required(ErrorMessage = "Input Required")]
        [UIHint("password")]
        public string UserPassword { get; set; }
        public bool? MailingList { get; set; }
        public int? UserRoleFk { get; set; } = 2;

        public virtual UserRole UserRoleFkNavigation { get; set; }
        public virtual ICollection<FilmReview> FilmReview { get; set; }
    }
}
