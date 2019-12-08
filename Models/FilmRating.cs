using System;
using System.Collections.Generic;

namespace Rockwell.Models
{
    public partial class FilmRating
    {
        public FilmRating()
        {
            Film = new HashSet<Film>();
        }

        public int RatingPk { get; set; }
        public string Rating { get; set; }

        public virtual ICollection<Film> Film { get; set; }
    }
}
