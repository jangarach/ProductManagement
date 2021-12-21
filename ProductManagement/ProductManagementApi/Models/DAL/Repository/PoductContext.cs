using ProductManagementApi.Models.DAL.DTO;
using System.Data.Entity;

namespace ProductManagementApi.Models.DAL.Repository
{
    public class PoductContext : DbContext
    {
        public PoductContext() : base("name=PoductContext")
        {
            Database.SetInitializer<PoductContext>(null);
        }

        public DbSet<Product> Products { get; set; }
    }
}