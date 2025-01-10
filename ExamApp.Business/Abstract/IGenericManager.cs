namespace ExamApp.Business.Abstract
{
    public interface IGenericManager<T, TDTO>
    {
        Task<T> GetByIdAsync(Guid Id);
        Task<List<T>> GetAllAsync();
        Task CreateAsync(TDTO entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
