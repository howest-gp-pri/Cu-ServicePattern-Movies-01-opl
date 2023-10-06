using Cu_ServicePattern_Movies_01.Core.Models;
using Cu_ServicePattern_Movies_01.Models;

namespace Cu_ServicePattern_Movies_01.ViewModels
{
    public class CartIndexViewModel
    {
        public List<CartItemModel> CartItems { get; set; }
        public decimal Total { get; set; }
    }
}
