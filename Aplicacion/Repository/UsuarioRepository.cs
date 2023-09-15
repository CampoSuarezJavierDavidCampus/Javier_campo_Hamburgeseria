using System.Linq.Expressions;
using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repositories;
public sealed class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository{
    public UsuarioRepository(DbAppContext context) : base(context){}

    public async Task<Usuario> GetByUsernameAsync(string nombre)=> await FindFirst(x => x.Nombre == nombre);
    protected override async Task<IEnumerable<Usuario>> GetAll(Expression<Func<Usuario, bool>> expression = null){
        if (expression is not null){
            return await _Entities                
                .Include(x => x.Roles)
                .Where(expression).ToListAsync();
        }
        return await _Entities
                .Include(x => x.Roles)
                .ToListAsync();
    }
}