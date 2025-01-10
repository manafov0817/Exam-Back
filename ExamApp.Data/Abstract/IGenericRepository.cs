namespace ExamApp.Data.Abstract
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(Guid Id);
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
