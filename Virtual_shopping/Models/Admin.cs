using System.ComponentModel.DataAnnotations;

namespace Virtual_Shopping.Models
{
    public class Admin
    {
		[Key]
		public int AdminID { get; set; }
		public string AdminName { get; set; }
		public string AdminEmail { get; set; }
		public string AdminPassword { get; set; }
	}
}
