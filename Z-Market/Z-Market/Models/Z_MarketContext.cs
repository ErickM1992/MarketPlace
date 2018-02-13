using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Z_Market.Models
{
    public class Z_MarketContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Z_MarketContext() : base("name=Z_MarketContext")
        {
        }

        //Borrando la caracteristica de borrado en cascada de la base de datos.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//Eliminado la remocion en cascada.
        }


        public System.Data.Entity.DbSet<Z_Market.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Z_Market.Models.DocumentType> DocumentTypes { get; set; }

        public System.Data.Entity.DbSet<Z_Market.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Z_Market.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<Z_Market.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Z_Market.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Z_Market.Models.OrderDetail> OrderDetail { get; set; }

        public System.Data.Entity.DbSet<Z_Market.Models.Category> Categories { get; set; }
    }
}
