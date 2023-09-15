using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>{
    public void Configure(EntityTypeBuilder<Usuario> builder){
        builder.ToTable("usuario");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("idPk");
        
        builder.Property(x => x.Nombre)
            .IsRequired()
            .HasColumnName("username")
            .HasMaxLength(50);
        
        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasMaxLength(200);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasMaxLength(100);
        
        builder.HasMany(p => p.Roles)
            .WithMany(p => p.Usuarios)
            .UsingEntity<UsuarioRoles>(
                t => t.HasOne(j => j.Rol)
                    .WithMany(j => j.UsuariosRoles)
                    .HasForeignKey(j => j.RolId),
                t => t.HasOne(j => j.Usuario)
                    .WithMany(j => j.UsuariosRoles)
                    .HasForeignKey(j => j.UsuarioId),
                t => {//--Configurations
                    t.ToTable("usuario_roles");
                    t.HasKey(j => new{j.RolId,j.UsuarioId});
        
                    t.Property(j => j.RolId)
                        .IsRequired()
                        .HasColumnName("rol_id");
                    
                    t.Property(j => j.UsuarioId)
                        .IsRequired()
                        .HasColumnName("usuario_id");
                }
            );
        
    }
}