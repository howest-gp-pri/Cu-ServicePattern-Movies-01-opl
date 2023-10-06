namespace Cu_ServicePattern_Movies_01.Core.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
