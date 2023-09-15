using System.Linq.Expressions;
using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Dominio.Interfaces.Pager;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repositories;
public sealed class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository{
    public CategoriaRepository(DbAppContext context) : base(context){}
    protected override async Task<IEnumerable<Categoria>> GetAll(Expression<Func<Categoria, bool>> expression = null){
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