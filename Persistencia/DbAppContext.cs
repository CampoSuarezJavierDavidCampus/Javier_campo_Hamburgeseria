using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;
public class DbAppContext : DbContext{
    public DbAppContext(DbContextOptions options) : base(options){}
    
    //-DbSets
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Chef> Chefs { get; set; }
    public DbSet<Hamburguesa> Hamburguesas { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    //-Configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
