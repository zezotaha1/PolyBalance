using PolyBalance.Repository;
using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<ICollection<T>> GetAllAsync();
    Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task<T> RestoreAsync(Expression<Func<T, bool>> predicate);
    Task<bool> IsUsedAsync(Expression<Func<T, bool>> predicate);
    Task<bool> IsIdValidTypeAsync<Type>(int id) where Type : class;

}
