using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cu_ServicePattern_Movies_01.Core
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        //many movies
        public ICollection<Movie> Movies { get; set; }
    }
}
