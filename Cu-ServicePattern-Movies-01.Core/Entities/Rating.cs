using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cu_ServicePattern_Movies_01.Core
{
    public class Rating : BaseEntity
    {
        public int Score { get; set; }
        public string Review { get; set; }
        //one movie
        public Movie Movie { get; set; }
        //unshadow foreign key
        public int MovieId { get; set; }
        //one user
        public User User { get; set; }
        public  int UserId { get; set; }
    }
}
