using Application.Repositories;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable{
    ICategoriaRepository _Categorias;
    IChefRepository _Chefs;
    IHamburguesaRepository _Hamburguesas;
    IIngredienteRepository _Ingredientes;
    IRolRepository _Roles;
    IUsuarioRepository _Usuario;

    private readonly DbAppContext _Context;

    public ICategoriaRepository Categorias => _Categorias ??= new CategoriaRepository(_Context);

    public IChefRepository Chefs => _Chefs ??= new ChefRepository(_Context);

    public IHamburguesaRepository Hamburguesas => _Hamburguesas ??= new HamburguesaRepository(_Context);

    public IIngredienteRepository Ingredientes => _Ingredientes ??= new IngredienteRepository(_Context);

    public IRolRepository Roles => _Roles ??= new RolRepository(_Context);

    public IUsuarioRepository Usuarios => _Usuario ??= new UsuarioRepository(_Context);

    public UnitOfWork(DbAppContext context)=> _Context = context;
    
    public void Dispose()=>_Context.Dispose();
    public async Task<int> SaveAsync()=>await _Context.SaveChangesAsync();

}