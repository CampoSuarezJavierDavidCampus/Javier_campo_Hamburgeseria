using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entities;
public class Hamburguesa: BaseEntity{
    [Column("decimal(8,2)")]
    public decimal Precio { get; set; }

    public int IdCategoria { get; set; }
    public Categoria Categoria { get; set; }

    public int IdChef { get; set; }
    public Chef Chef { get; set; }

    public ICollection<Ingredientes> Ingredientes { get; set; } = new HashSet<Ingredientes>();
    public ICollection<HamburguesaIngredientes> HamburguesaIngredientes { get; set; }
}
