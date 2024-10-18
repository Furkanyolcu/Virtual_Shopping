using System.ComponentModel.DataAnnotations;

namespace Virtual_Shopping.Models
{
	public class Products
	{
		[Key]
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public string ProductImageURL { get; set; }
		public float ProductPrice { get; set; }
		public int ProductCategoryID { get; set; }
		public List<Seller> Sellers { get; set; }

	}
}
