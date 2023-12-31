using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces.Pager;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public abstract class  GenericRepository<T> where T : BaseEntity{
    private readonly DbAppContext _context;
    protected readonly DbSet<T> _Entities;    
    public GenericRepository(DbAppContext context){
        _context = context;
        _Entities = _context.Set<T>();
    }   

    public async virtual Task<T> FindFirst(Expression<Func<T, bool>> expression){
        if (expression is not null){
            return await _Entities.Where(expression).FirstAsync();
        }
        return await _Entities.FindAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)=> await _Entities.FindAsync(id);
    public async virtual void Add(T entity) => await _Entities.AddAsync(entity);
    public async virtual void AddRange(IEnumerable<T> entities)=> await _Entities.AddRangeAsync(entities);
    public virtual void Remove(T entity) => _Entities.Remove(entity);
    public virtual void RemoveRange(IEnumerable<T> entities)=> _Entities.RemoveRange(entities);
    public virtual void Update(T entity)=> _Entities.Update(entity);

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, IParam param = null){
        if(param is null){
            return await GetAll(expression);
        }
        return await GetAllPaginated(param,expression);

    }
    protected virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null){
        if (expression is not null){
            return await _Entities.Where(expression).ToListAsync();
        }
        return await _Entities.ToListAsync();
    }

    private async Task<IEnumerable<T>> GetAllPaginated(IParam param, Expression<Func<T, bool>> expression = null ){        
        return (await GetAll(expression))                                
                .Where(x => x.Nombre.Trim().ToLower().StartsWith(param.Search.Trim().ToLower()) )                
                .Skip((param.PageIndex - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToList();
            
    }

}
