namespace Shopping_Tutorial.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string Ordercode { get; set; }
		public string UserName { get; set; }
		public DateTime CreateDate { get; set; }
		public int Status { get; set; }
	}
}
