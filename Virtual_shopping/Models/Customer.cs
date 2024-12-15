using System.ComponentModel.DataAnnotations;

namespace Virtual_Shopping.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; } 
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public Boolean IsActive { get; set; }
        public Token Token { get; set; }

    }
}
