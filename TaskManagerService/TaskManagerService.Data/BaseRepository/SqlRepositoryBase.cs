using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerService.Data.BaseRepository
{
    public class SqlRepositoryBase<TContext, TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TContext : DbContext
        where TEntity : class, IEntity<TKey>
    {
        protected readonly TContext dbContext;

        public SqlRepositoryBase(TContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected IQueryable<TEntity> Entities => dbContext.Set<TEntity>().AsNoTracking();
        
        public Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken)
        {
            return dbContext.FindAsync<TEntity>(key).AsTask();
        }

        public async Task<IEnumerable<TEntity>> ListAsync(int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            return await Entities.OrderBy(a => a.Id).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public Task AddAsync(TEntity item, CancellationToken cancellationToken)
        {
            return dbContext.AddAsync(item, cancellationToken).AsTask();
        }

        public Task UpdateAsync(TEntity item, CancellationToken cancellationToken)
        {
            return Task.FromResult(dbContext.Update(item));
        }

        public async Task DeleteAsync(TKey key, CancellationToken cancellationToken)
        {
            await Task.FromResult(dbContext.Remove(await GetAsync(key, cancellationToken)));
        }
    }
}