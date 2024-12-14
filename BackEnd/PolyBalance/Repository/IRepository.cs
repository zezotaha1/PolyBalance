using PolyBalance.Repository;
using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<ICollection<T>> GetAllAsync();
   Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task RestoreAsync(Expression<Func<T, bool>> predicate);
}
