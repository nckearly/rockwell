using System;
using System.Collections.Generic;

namespace Rockwell.Models
{
    public partial class FilmActor
    {
        public int FilmActorPk { get; set; }
        public int FilmFk { get; set; }
        public int ActorFk { get; set; }
        public bool? IsStarringRole { get; set; }
        public decimal? Salary { get; set; }

        public virtual Actor ActorFkNavigation { get; set; }
        public virtual Film FilmFkNavigation { get; set; }
    }
}
