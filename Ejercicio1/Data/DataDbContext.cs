using Microsoft.EntityFrameworkCore;

namespace Ejercicio1.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {


        }

        public DbSet<Producto> productos { get; set; }
 
        public DbSet<Categoria> categorias { get; set; }
    }
}
