using Microsoft.AspNetCore.Mvc.Rendering;
using Cu_ServicePattern_Movies_01.Models;
using System.ComponentModel.DataAnnotations;

namespace Cu_ServicePattern_Movies_01.ViewModels
{
    public class MoviesCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Date of release")]
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
        [Display(Name = "Companies")]
        public int CompanyId { get; set; }
        public IEnumerable<SelectListItem> Actors { get; set; }
        [Display(Name = "Actors")]
        [Required]
        public IEnumerable<int> ActorIds { get; set; }
        [Required]
        public List<CheckboxModel> Directors { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }
    }
}
