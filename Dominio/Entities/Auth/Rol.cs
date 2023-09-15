namespace Dominio.Entities;
public class Rol:BaseEntity{
    public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    public ICollection<UsuarioRoles> UsuariosRoles { get; set; }
}
