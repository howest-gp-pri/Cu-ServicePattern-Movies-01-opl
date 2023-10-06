using Microsoft.AspNetCore.Mvc;

namespace Cu_ServicePattern_Movies_01.Models
{
    public class CheckboxModel
    {
        [HiddenInput]
        public int Value { get; set; }
        [HiddenInput]
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }
}
