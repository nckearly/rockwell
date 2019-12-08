using System;
using System.Collections.Generic;

namespace Rockwell.Models
{
    public partial class Actor
    {
        public Actor()
        {
            FilmActor = new HashSet<FilmActor>();
        }

        public int ActorPk { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public byte Age { get; set; }
        public string Gender { get; set; }
        public string ActorAgent { get; set; }
        public string NameFirstReal { get; set; }
        public string NameLastReal { get; set; }
        public byte? AgeReal { get; set; }
        public bool? IsEgomaniac { get; set; }
        public bool? IsTotalBabe { get; set; }

        public virtual ICollection<FilmActor> FilmActor { get; set; }
    }
}
