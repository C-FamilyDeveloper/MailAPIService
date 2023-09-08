namespace MailAPIService.Models.Interfaces
{
    public interface IBaseRepository <T> where T : class
    {

        Task Add (T entity);

        Task<T> AddIfNotExist(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<T?> Find(T entity);

        Task<List<T>> Get();
        Task Save();
    }
}
