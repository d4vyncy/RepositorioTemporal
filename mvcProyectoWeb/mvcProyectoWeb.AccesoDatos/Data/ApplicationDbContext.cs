using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mvcProyectoWeb.Models;

namespace mvcProyectoWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //agregar aqui los modelos que se crean en la base de datos
        public DbSet<Almacen> Almacen { get; set; }
        public DbSet<Producto> Producto { get; set; }
    }
}
