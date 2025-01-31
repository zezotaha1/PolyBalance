using PolyBalance.Repository;
using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task<T> RestoreAsync(Expression<Func<T, bool>> predicate);
    Task<bool> IsUsedAsync(Expression<Func<T, bool>> predicate);
    Task<bool> IsIdValidTypeAsync<Type>(int id) where Type : class;

}
