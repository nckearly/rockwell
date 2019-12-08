using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockwell.Models
{
    public class Movie
    {
        public int ID
        {
            get; set;
            
        }
        public string Name { get; set; }
        public string PitchText { get; set; }
        public int Budget { get; set; }
    }
}
