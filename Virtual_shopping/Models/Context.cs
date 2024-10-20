using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Virtual_Shopping.Models
{
    public class Context : DbContext
    {
        #region FurkanDB

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=DESKTOP-8EQ0RI8\\SQLEXPRESS; database=ShoppingDB; integrated security=true; TrustServerCertificate=true");
        //}
        #endregion

        #region SadıkDB

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost; database=ShoppingDB; integrated security=true; TrustServerCertificate=True;");
        }
        #endregion

        #region MehmetAliDB
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=SAMANCI\\SQLEXPRESS; database=ShoppingDB; integrated security=true; TrustServerCertificate=True");
        //}
        #endregion
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }

	}
}

