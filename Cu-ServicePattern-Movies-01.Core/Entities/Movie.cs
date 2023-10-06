using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cu_ServicePattern_Movies_01.Core
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        //many
        public ICollection<Rating> Ratings { get; set; }
        //one company
        public Company Company { get; set; }
        public int? CompanyId { get; set; }
        //many to many actors
        public ICollection<Actor> Actors { get; set; }
        //many to many directors
        public ICollection<Director> Directors { get; set; }
        //image
        public string Image { get; set; }
        //price
        public decimal Price { get; set; }

    }
}
