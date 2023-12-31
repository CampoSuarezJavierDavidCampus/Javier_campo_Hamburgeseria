using Dominio.Entities;

using System.Linq.Expressions;
using Dominio.Interfaces.Pager;

namespace Dominio.Interfaces;
public interface IGenericRepository<T> where T : BaseEntity{        
    Task<T> FindFirst(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, IParam param = null);
    Task<T> GetByIdAsync(int id);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}

