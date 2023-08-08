using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    //Agregar modelos debajo
    public DbSet<Category> Categorias { get; set; }
    public DbSet<Pelicula> Pelicula { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
}