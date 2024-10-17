using System.ComponentModel.DataAnnotations;

namespace Virtual_Shopping.Models
{
	public class Seller
	{
		[Key]
		public int SellerID { get; set; }
		public string SellerName { get; set; }
		public string SellerEmail { get; set; }
		public string SellerPassword { get; set; }
		public Products[] SellerProducts { get; set; }
	}
}
