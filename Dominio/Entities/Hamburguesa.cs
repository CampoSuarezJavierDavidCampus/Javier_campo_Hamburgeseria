namespace Dominio.Entities;
public class Hamburguesa: BaseEntity{    
    public double Precio { get; set; }

    public int IdCategoria { get; set; }
    public Categoria Categoria { get; set; }

    public int IdChef { get; set; }
    public Chef Chef { get; set; }

    public ICollection<Ingrediente> Ingrediente { get; set; } = new HashSet<Ingrediente>();
    public ICollection<HamburguesaIngredientes> HamburguesaIngredientes { get; set; }
}
