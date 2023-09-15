using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Application.Repositories;
public sealed class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository{
    public UsuarioRepository(DbAppContext context) : base(context){}

    public async Task<Usuario> GetByUsernameAsync(string nombre)=> await FindFirst(x => x.Nombre == nombre);
}