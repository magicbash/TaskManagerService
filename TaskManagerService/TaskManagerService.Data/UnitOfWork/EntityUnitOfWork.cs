using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerService.Data.UnitOfWork
{
    public class EntityUnitOfWork : UnitOfWorkBase
    {
        private readonly DbContext[] _dbContexts;

        public EntityUnitOfWork(DbContext[] dbContexts)
        {
            _dbContexts = dbContexts;
        }

        public override async Task CompleteAsync(CancellationToken cancellationToken)
        {
            var tasks = _dbContexts.Select(dbContext => dbContext.SaveChangesAsync(cancellationToken)).ToArray();

            await Task.WhenAll(tasks);
        }

        protected override bool IsDirty()
        {
            return _dbContexts.Any(dbContext => dbContext.ChangeTracker.HasChanges());
        }
    }
}