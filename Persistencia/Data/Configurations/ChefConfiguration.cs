using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;
public class ChefConfiguration : IEntityTypeConfiguration<Chef>{
    public void Configure(EntityTypeBuilder<Chef> builder){
        builder.ToTable("chef");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("idPk");
        
        builder.Property(x => x.Nombre)
            .IsRequired()
            .HasColumnName("nombre")
            .HasMaxLength(50);
        
        builder.Property(x => x.Especialidad)
            .IsRequired()
            .HasColumnName("especialidad")
            .HasMaxLength(100);                
    }
}