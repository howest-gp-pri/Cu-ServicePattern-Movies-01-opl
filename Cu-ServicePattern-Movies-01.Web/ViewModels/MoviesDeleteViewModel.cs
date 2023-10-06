using Microsoft.AspNetCore.Mvc;

namespace Cu_ServicePattern_Movies_01.ViewModels
{
    public class MoviesDeleteViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
