using Microsoft.AspNetCore.Mvc;

namespace Cu_ServicePattern_Movies_01.ViewModels
{
    public class MoviesUpdateViewModel : MoviesCreateViewModel
    {
        //movie id to update
        [HiddenInput]
        public int Id { get; set; }
    }
}
