using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagerService.Data.BaseRepository
{
    /// <summary>
    /// Repository that return data
    /// </summary>
    /// <typeparam name="TEntity">Class of entity that you need</typeparam>
    /// <typeparam name="TKey">type of key of this entity</typeparam>
    public interface IQueryRepository<TEntity, in TKey>
    where TEntity: IEntity<TKey>
    {
        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <param name="key">Id of entity</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Entity with id equal key</returns>
        Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken);
        
        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <param name="pageSize">Number of entities that you want to get</param>
        /// <param name="pageIndex">Number of page, start form 0</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of entities with pagination</returns>
        Task<IEnumerable<TEntity>> ListAsync(int pageSize, int pageIndex, CancellationToken cancellationToken);
    }
}