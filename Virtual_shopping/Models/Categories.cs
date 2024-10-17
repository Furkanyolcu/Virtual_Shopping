using System.ComponentModel.DataAnnotations;

namespace Virtual_Shopping.Models
{
	public class Categories
	{
		[Key]
		public int CategoryID { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
	}
}
