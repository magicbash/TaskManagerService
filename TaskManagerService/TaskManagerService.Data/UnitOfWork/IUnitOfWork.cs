using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagerService.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync(CancellationToken cancellationToken);
    }
}