using Cu_ServicePattern_Movies_01.ViewModels;

namespace Cu_ServicePattern_Movies_01.ViewModels
{
    public class MoviesInfoViewModel : BaseViewModel
    {
        public BaseViewModel Company { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string PageTitle { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
