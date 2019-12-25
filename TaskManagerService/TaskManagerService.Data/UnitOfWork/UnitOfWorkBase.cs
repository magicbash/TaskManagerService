using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagerService.Data.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public abstract Task CompleteAsync(CancellationToken cancellationToken);

        public void Dispose()
        {
            if (IsDirty())
                throw new Exception("Unit of work disposed without completion.");
        }

        protected abstract bool IsDirty();
    }
}