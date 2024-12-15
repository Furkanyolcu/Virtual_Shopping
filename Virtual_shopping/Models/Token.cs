using System;

namespace Virtual_Shopping.Models
{
    public class Token
    {
        public int TokenID { get; set; } // Benzersiz bir kimlik
        public string Value { get; set; } // Token değeri (JWT veya rastgele bir string)
        public DateTime CreatedDate { get; set; } // Token oluşturulma tarihi
        public DateTime ExpirationDate { get; set; } // Tokenin geçerlilik süresi
        public bool IsActive { get; set; } // Tokenin aktif olup olmadığını belirtir
        public int CustomerID { get; set; } // Tokenin bağlı olduğu müşterinin ID'si

        // Navigation property (Entity Framework ile ilişkilendirme)
        public Customer Customer { get; set; } // Müşteriyle ilişkilendirme
    }
}
