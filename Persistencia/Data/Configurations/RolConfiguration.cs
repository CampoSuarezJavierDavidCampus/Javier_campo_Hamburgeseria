using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;
public class RolConfiguration : IEntityTypeConfiguration<Rol>{
    public void Configure(EntityTypeBuilder<Rol> builder){
        builder.ToTable("rol");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("idPk");
        
        builder.Property(x => x.Nombre)
            .IsRequired()
            .HasColumnName("nombre")
            .HasMaxLength(50);
                
    }
}