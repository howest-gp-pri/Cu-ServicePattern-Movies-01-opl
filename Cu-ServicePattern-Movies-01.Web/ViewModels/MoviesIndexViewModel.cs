namespace Cu_ServicePattern_Movies_01.ViewModels
{
    public class MoviesIndexViewModel
    {
        public IEnumerable<MoviesInfoViewModel> Movies { get; set; }
        public string PageTitle { get; set; }
    }
}
