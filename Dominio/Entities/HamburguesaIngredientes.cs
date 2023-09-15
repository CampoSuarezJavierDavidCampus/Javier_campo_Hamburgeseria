namespace Dominio.Entities;
public class HamburguesaIngredientes{
    public int IdHamburguesa { get; set; }
    public Hamburguesa Hamburguesa { get; set; }
    
    public int IdIngredientes { get; set; }
    public Ingredientes Ingrediente { get; set; }
    
}
