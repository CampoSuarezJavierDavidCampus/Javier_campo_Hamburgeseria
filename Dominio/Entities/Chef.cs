namespace Dominio.Entities;
public class Chef: BaseEntity{
    public string Especialidad { get; set; } = String.Empty;
    public ICollection<Hamburguesa> Hamburguesas {set; get;} = new HashSet<Hamburguesa>();
}
