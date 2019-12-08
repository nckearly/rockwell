using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;

namespace Rockwell.Models
{
    public class LoginInput
    {

        [Required(ErrorMessage ="Input Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Input Required")]
        [UIHint("password")]
        public string UserPassword { get; set; }
        public string ReturnURL { get; set; }

    }
}
