namespace API.Dtos;
public class HamburguesaSimpleDto{    
    public int Id { get; set; }
    public string Nombre { get; set; } = String.Empty;
    public decimal Precio { get; set; }    
    public int IdCategoria { get; set; }
    public int IdChef { get; set; }
    

    public List<IngredienteDto> Ingredientes { get; set; } = null;
}
