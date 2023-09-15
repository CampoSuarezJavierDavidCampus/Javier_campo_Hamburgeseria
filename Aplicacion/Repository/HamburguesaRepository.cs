using System.Linq.Expressions;
using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Dominio.Interfaces.Pager;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repositories;
public sealed class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesaRepository{
    public HamburguesaRepository(DbAppContext context) : base(context){}

    protected override async Task<IEnumerable<Hamburguesa>> GetAll(Expression<Func<Hamburguesa, bool>> expression = null){
        if (expression is not null){
            return await _Entities
                .Include(x => x.Chef)
                .Include(x => x.Categoria)
                .Include(x => x.Ingrediente)
                .Where(expression).ToListAsync();
        }
        return await _Entities
                .Include(x => x.Chef)
                .Include(x => x.Ingrediente)
                .Include(x => x.Categoria)
                .ToListAsync();
    }


}