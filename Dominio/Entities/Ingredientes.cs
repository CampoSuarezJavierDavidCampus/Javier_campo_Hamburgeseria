using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entities;
public class Ingredientes: BaseEntity{
    public string Descripcion { get; set; } = String.Empty;
    
    [Column("decimal(8,2)")]
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public ICollection<Hamburguesa> Hamburguesas { get; set; } = new HashSet<Hamburguesa>();
    public ICollection<HamburguesaIngredientes> HamburguesaIngredientes { get; set; }
}
