using System.Threading;
using System.Threading.Tasks;

namespace TaskManagerService.Data.BaseRepository
{
    /// <summary>
    /// Repository that delete data
    /// </summary>
    /// <typeparam name="TKey">Type of key of this entity</typeparam>
    public interface IDeleteRepository<TKey>
    {
        /// <summary>
        /// Delete entity with id equal key
        /// </summary>
        /// <param name="key">id of entity that you want to delete</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(TKey key, CancellationToken cancellationToken);
    }
}