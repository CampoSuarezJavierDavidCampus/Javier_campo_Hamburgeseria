

using Dominio.Entities;

namespace Dominio.Interfaces;
using System.Linq.Expressions;
using Dominio.Interfaces.Pager;

public interface IGenericRepository<T> where T : BaseEntity{        
    Task<T> FindFirst(Expression<Func<T, bool>> expression);
    IAsyncEnumerable<T> GetAllAsync(Expression<Func<T, bool>> expression, IParam param = null );
    IAsyncEnumerable<T> GetAllAsync(IParam param = null);
    
    Task<T> GetByIdAsync(int id);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}

