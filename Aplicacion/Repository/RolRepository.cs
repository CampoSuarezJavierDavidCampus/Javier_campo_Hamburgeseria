using System.Linq.Expressions;
using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repositories;
public sealed class RolRepository : GenericRepository<Rol>, IRolRepository{
    public RolRepository(DbAppContext context) : base(context){}
    protected override async Task<IEnumerable<Rol>> GetAll(Expression<Func<Rol, bool>> expression = null){
        if (expression is not null){
            return await _Entities                
                .Include(x => x.Usuarios)
                .Where(expression).ToListAsync();
        }
        return await _Entities
                .Include(x => x.Usuarios)
                .ToListAsync();
    }
}