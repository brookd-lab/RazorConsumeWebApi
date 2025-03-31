using Microsoft.EntityFrameworkCore;
using Product_Tutorial.Models;

namespace RazorConsumeWebApi.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
        { }
        public DbSet<ProductTable> productTables { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
