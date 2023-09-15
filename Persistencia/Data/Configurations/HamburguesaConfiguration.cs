using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;
public class HamburguesaConfiguration : IEntityTypeConfiguration<Hamburguesa>{
    public void Configure(EntityTypeBuilder<Hamburguesa> builder){
        builder.ToTable("hamburguesa");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("idPk");
        
        builder.Property(x => x.Nombre)
            .IsRequired()
            .HasColumnName("nombre")
            .HasMaxLength(50);
        
        builder.Property(x => x.IdChef)
            .IsRequired()
            .HasColumnName("IdChef");
        
        builder.Property(x => x.IdCategoria)
            .IsRequired()
            .HasColumnName("IdCategoria");
        
        builder.Property(x => x.Precio)
            .IsRequired()
            .HasColumnName("precio");

        builder.HasOne(x => x.Chef)
            .WithMany(x => x.Hamburguesas)
            .HasForeignKey(x => x.IdChef);
        
        builder.HasOne(x => x.Categoria)
            .WithMany(x => x.Hamburguesas)
            .HasForeignKey(x => x.IdCategoria); 

        builder.HasMany(p => p.Ingrediente)
            .WithMany(p => p.Hamburguesas)
            .UsingEntity<HamburguesaIngredientes>(
                t => t.HasOne(j => j.Ingrediente)
                    .WithMany(j => j.HamburguesaIngredientes)
                    .HasForeignKey(j => j.IdIngrediente),
                t => t.HasOne(j => j.Hamburguesa)
                    .WithMany(j => j.HamburguesaIngredientes)
                    .HasForeignKey(j => j.IdHamburguesa),
                t => {//--Configurations
                    t.ToTable("hamburguesa_ingredientes");
                    t.HasKey(j => new{j.IdIngrediente,j.IdHamburguesa});

                    t.Property(t => t.IdIngrediente)
                        .IsRequired()
                        .HasColumnName("ingrediente_id");
                    
                    t.Property(t => t.IdHamburguesa)
                        .IsRequired()
                        .HasColumnName("hamburguesa_id");                    
                }
            );       
    }
}