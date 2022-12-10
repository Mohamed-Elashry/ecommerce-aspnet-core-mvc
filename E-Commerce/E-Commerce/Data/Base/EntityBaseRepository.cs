
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace E_Commerce.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            var result = await _context.Set<T>().FindAsync(Id);
            if (result != null)
                return result;
            else
                return new T();
        }

        public async Task<bool> AddAsync(T entity)
        {
            if (entity != null)
            {
                await _context.Set<T>().AddAsync(entity);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

       
        public async Task<bool> UpdateAsync(int Id, T updatedEntity)
        {
            try
            {
                if (updatedEntity.Id > 0)
                {
                    EntityEntry entityEntry = _context.Entry<T>(updatedEntity);
                    entityEntry.State = EntityState.Modified;
                    var result = await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int Id)
        {
            var entity = await _context.Set<T>().FindAsync(Id);
            if (entity != null)
            {
                EntityEntry entityEntry = _context.Entry<T>(entity);
                entityEntry.State = EntityState.Deleted;
                var result = await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query,(current,includeProperties) => current.Include(includeProperties));
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, Expression<Func<T,object>>[] includeProperties)
        {
            IQueryable<T> result =  _context.Set<T>().Where(filter);
            //.Include(includeProperties)
            //.Where(filter).ToListAsync();
            result = includeProperties.Aggregate(result, (current, includeProperties) => current.Include(includeProperties));
            return await result.ToListAsync();
        }
    }
}
