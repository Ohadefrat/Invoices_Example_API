using Microsoft.EntityFrameworkCore;

namespace Invoice_API.Controllers.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Invoices> Invoices => Set<Invoices>();
    }
}
