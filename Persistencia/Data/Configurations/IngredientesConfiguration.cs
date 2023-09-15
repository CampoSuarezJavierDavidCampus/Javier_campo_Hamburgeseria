using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;
public class IngredientesConfiguration : IEntityTypeConfiguration<Ingrediente>{
    public void Configure(EntityTypeBuilder<Ingrediente> builder){
        builder.ToTable("ingredientes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("idPk");
        
        builder.Property(x => x.Nombre)
            .IsRequired()
            .HasColumnName("nombre")
            .HasMaxLength(50);
        
        builder.Property(x => x.Precio)
            .IsRequired()
            .HasColumnName("precio");            
        
        builder.Property(x => x.Stock)
            .IsRequired()
            .HasColumnName("stock");        
        
        
    }
}