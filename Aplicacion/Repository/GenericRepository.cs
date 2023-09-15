using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces.Pager;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public abstract class  GenericRepository<T> where T : BaseEntity{
    private readonly DbAppContext _context;
    private readonly DbSet<T> _Entities;
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
    public virtual async IAsyncEnumerable<T> GetAllAsync(IParam param = null){        
        if (param !=null){
            await foreach (
                var item in _Entities
                .Skip((param.PageIndex - 1) * param.PageSize)
                .Take(param.PageSize)
                .AsAsyncEnumerable()
            ){
                yield return item;
            }
        }else{
            var records = _Entities;
            await foreach (var record in records){
                yield  return  record;
            }
        }        
    }

    public virtual async IAsyncEnumerable<T> GetAllAsync(Expression<Func<T, bool>> expression, IParam param = null){
        if(param != null){           
            await foreach (
                var item in _Entities
                .Where(expression)
                .Skip((param.PageIndex - 1) * param.PageSize)
                .Take(param.PageSize)
                .AsAsyncEnumerable()
            ){
                yield return item;
            } 
        }else{            
            await foreach (
                var record in _Entities.Where(expression).AsAsyncEnumerable()){
                yield return record;
            }
        }
    }       


}
