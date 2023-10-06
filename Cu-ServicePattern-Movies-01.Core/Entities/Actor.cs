using Cu_ServicePattern_Movies_01.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cu_ServicePattern_Movies_01.Core
{
    public class Actor : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        //many to many movies
        public ICollection<Movie> Movies { get; set; }
    }
}
