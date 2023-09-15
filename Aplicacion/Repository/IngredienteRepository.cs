using System.Linq.Expressions;
using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repositories;
public sealed class IngredienteRepository : GenericRepository<Ingrediente>, IIngredienteRepository{
    public IngredienteRepository(DbAppContext context) : base(context){}
    protected override async Task<IEnumerable<Ingrediente>> GetAll(Expression<Func<Ingrediente, bool>> expression = null){
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