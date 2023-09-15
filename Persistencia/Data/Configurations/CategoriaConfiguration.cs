using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;
public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>{
    public void Configure(EntityTypeBuilder<Categoria> builder){
        builder.ToTable("categoria");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("idPk");
            
        builder.Property(x => x.Nombre)
            .IsRequired()
            .HasColumnName("nombre")
            .HasMaxLength(50);
        
        builder.Property(x => x.Descripcion)
            .IsRequired()
            .HasColumnName("descripcion")
            .HasMaxLength(100);                        
    }
}
