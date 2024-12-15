using System.ComponentModel.DataAnnotations;

namespace Virtual_Shopping.Models
{
	public class Notification
	{
		[Key]
		public int NotificationID { get; set; }
		public string NotificationMessage { get; set; }
		public DateTime? CreateTime { get; set; }
		public bool IsRead { get; set; }
	}
}
