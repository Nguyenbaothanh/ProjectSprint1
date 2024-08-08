namespace Shopping.Models.ViewModels
{
	public class CartItemViewModel
	{
		public List<CartItemViewModel> CartItems { get; set; }
		public decimal GrandTotal { get; set; }
	}
}
