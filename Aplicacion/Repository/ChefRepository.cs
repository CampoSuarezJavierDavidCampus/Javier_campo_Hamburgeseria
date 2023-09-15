using System.Linq.Expressions;
using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repositories;
public sealed class ChefRepository : GenericRepository<Chef>, IChefRepository{
    public ChefRepository(DbAppContext context) : base(context){}
    protected override async Task<IEnumerable<Chef>> GetAll(Expression<Func<Chef, bool>> expression = null){
        if (expression is not null){
            return await _Entities                
                .Include(x => x.Hamburguesas)
                .Where(expression).ToListAsync();
        }
        return await _Entities
                .Include(x => x.Hamburguesas)
                .ToListAsync();
    }
}