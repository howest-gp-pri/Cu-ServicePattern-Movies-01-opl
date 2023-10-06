using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cu_ServicePattern_Movies_01.Core
{
    public class User : BaseEntity
    {
        public  string Username { get; set; }
        public  string Firstname { get; set; }
        public  string Lastname{ get; set; }
        //many ratings
        public ICollection<Rating> Ratings { get; set; }
    }

}
