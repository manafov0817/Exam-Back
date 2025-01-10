using ExamApp.Data.Abstract;
using ExamApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Data.Concrete.EfCore
{
    public class EfCoreGenericRepository<T> : IGenericRepository<T>
       where T : BaseEntity
    {
        private readonly EfCoreDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfCoreGenericRepository(EfCoreDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        { 
            var data = await _context.Set<T>().ToListAsync();
            return data;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
