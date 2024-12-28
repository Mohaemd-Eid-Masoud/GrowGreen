namespace GrowGreen.Web.ViewModels
{
    public class CartViewModel
    {
        public int UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public decimal TotalPrice => Items?.Sum(item => item.TotalPrice) ?? 0;
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }
}
