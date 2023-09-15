namespace Dominio.Entities;
public class Categoria: BaseEntity{
    public string Descripcion { get; set; } = String.Empty;
    public ICollection<Hamburguesa> Hamburguesas {set; get;} = new HashSet<Hamburguesa>();
}
