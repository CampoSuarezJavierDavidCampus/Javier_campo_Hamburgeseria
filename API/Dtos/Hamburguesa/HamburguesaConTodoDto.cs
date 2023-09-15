namespace API.Dtos;
public class HamburguesaConTodoDto: HamburguesaSimpleDto{    
    public CategoriaDto Categoria { get; set; }
    public ChefDto Chef { get; set; }
    public List<IngredienteDto> Ingredientes { get; set; }

}
