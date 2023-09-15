namespace API.Dtos;
public class IngredienteDto{
    public int Id { get; set; }
    public string Descripcion { get; set; } = String.Empty;        
    public decimal Precio { get; set; }
    public int Stock { get; set; }    
}
