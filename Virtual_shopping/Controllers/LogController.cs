using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Virtual_Shopping.Models;

namespace Virtual_Shopping.Controllers
{
	public class LogController : Controller
	{
		Context c = new Context();

		[HttpPost]
		public async Task LoginLog(string Mail, string Role,bool Status)
		{
			if(Role == "Admin")
			{
				if (Status)
				{
					c.Logs.Add(new Logs
					{
						LogCategory = "AdminLogin",
						Activity = "Admin " + Mail + " sisteme giris yaptı",
						LogDate = DateTime.Now
					});
					await c.SaveChangesAsync();
				}
				else
				{
					c.Logs.Add(new Logs
					{
						LogCategory = "AdminLogin",
						Activity = Mail + " sisteme giris yapmaya çalıştı",
						LogDate = DateTime.Now
					});
					await c.SaveChangesAsync();
				}
				
			}
			else if(Role == "User")
			{
				if (Status)
				{
					c.Logs.Add(new Logs
					{
						LogCategory = "UserLogin",
						Activity = "User " + Mail + " sisteme giris yaptı",
						LogDate = DateTime.Now
					});
					await c.SaveChangesAsync();
				}
				else
				{
					c.Logs.Add(new Logs
					{
						LogCategory = "UserLogin",
						Activity = Mail + " sisteme giris yapmaya çalıştı",
						LogDate = DateTime.Now
					});
					await c.SaveChangesAsync();
				}

			}
			else if (Role == "Seller")
			{
				if (Status)
				{
					c.Logs.Add(new Logs
					{
						LogCategory = "SellerLogin",
						Activity = "Seller " + Mail + " sisteme giris yaptı",
						LogDate = DateTime.Now
					});
					await c.SaveChangesAsync();
				}
				else
				{
					c.Logs.Add(new Logs
					{
						LogCategory = "SellerLogin",
						Activity = Mail + " sisteme giris yapmaya çalıştı",
						LogDate = DateTime.Now
					});
					await c.SaveChangesAsync();
				}
			}
		}

		[HttpPost]
		public IActionResult DeleteNotification(int id)
		{
			var log = c.Logs.Find(id);
			if (log != null)
			{
				c.Logs.Remove(log);
				c.SaveChanges();
			}
			return RedirectToAction("Logs");
		}
	}
}
