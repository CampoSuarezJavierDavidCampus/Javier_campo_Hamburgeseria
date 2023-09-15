
namespace Dominio.Entities;
public class Ingrediente: BaseEntity{
    public string Descripcion { get; set; } = String.Empty;
        
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public ICollection<Hamburguesa> Hamburguesas { get; set; } = new HashSet<Hamburguesa>();
    public ICollection<HamburguesaIngredientes> HamburguesaIngredientes { get; set; }
}
