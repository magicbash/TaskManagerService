using System.Threading;
using System.Threading.Tasks;

namespace TaskManagerService.Business
{
    public interface IProjectStateUpdater
    {
        Task UpdateProjectStateAsync(int projectId, CancellationToken cancellationToken);
    }
}