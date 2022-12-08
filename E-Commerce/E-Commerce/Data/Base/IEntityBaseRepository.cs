using System.Linq.Expressions;

namespace E_Commerce.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int Id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(int Id, T entity);
        Task<bool> DeleteAsync(int Id);
    }
}
