using System.Threading;
using System.Threading.Tasks;

namespace TaskManagerService.Data.BaseRepository
{
    /// <summary>
    /// Repository that change data
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public interface IAddUpdateRepository<T>
    {
        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="item">new entity</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(T item, CancellationToken cancellationToken);
        /// <summary>
        /// change existing entity
        /// </summary>
        /// <param name="item">entity to change</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(T item, CancellationToken cancellationToken);
    }
}