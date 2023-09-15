namespace Dominio.Interfaces;
public interface IUnitOfWork{
    ICategoriaRepository Categorias {get;}    
    IChefRepository Chefs {get;}
    IHamburguesaRepository Hamburguesas {get;}
    IIngredientesRepository Ingredientes {get;}
    IRolRepository Roles {get;}
    IUsuarioRepository Usuario {get;}
    Task<int> SaveAsync();
}

