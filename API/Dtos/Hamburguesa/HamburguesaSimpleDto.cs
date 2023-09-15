namespace API.Dtos;
public class HamburguesaSimpleDto{    
    public string Nombre { get; set; } = String.Empty;
    public decimal Precio { get; set; }
    public List<IngredienteDto> Ingredientes { get; set; }
}
