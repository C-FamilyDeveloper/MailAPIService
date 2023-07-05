namespace MailerAPIService.Models.Interfaces
{
    public interface IBaseRepository <T> where T : class
    {
        /// <summary>
        /// Добавляет сущность в БД
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
        Task Add (T entity);
        /// <summary>
        /// Добавляет сущность в БД, если ее там нет
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        /// <returns>(awaitable) Асинхронная задача, возвращающая сущность из БД</returns>
        Task<T> AddIfNotExist(T entity);
        /// <summary>
        /// Редактирует сущность в БД
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
        Task Update(T entity);
        /// <summary>
        /// Удаляет сущность из БД
        /// </summary>
        /// <param name="entity">Удаляемая сущность</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
        Task Delete(T entity);
        /// <summary>
        /// Находит сущность в БД
        /// </summary>
        /// <param name="entity">Удаляемая сущность</param>
        /// <returns>(awaitable) Асинхронная задача, возвращающая сущность, если она найдена </returns>
        Task<T?> Find(T entity);
        /// <summary>
        /// Получает все записи таблицы из БД
        /// </summary>
        /// <returns>Коллекция сущностей</returns>
        IQueryable<T> GetAllEntities();
    }
}
