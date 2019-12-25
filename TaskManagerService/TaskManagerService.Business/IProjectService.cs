using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManagerService.Data.Enitites;
using TaskManagerService.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagerService.Business
{
    public interface IProjectService
    {
        Task<ProjectViewModel> GetProjectAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ProjectViewModel>> ListAsync(int pageSize, int pageIndex, CancellationToken cancellationToken);
        Task DeleteProjectAsync(int id, CancellationToken cancellationToken);
        Task CreateProjectAsync(ProjectViewModel project, CancellationToken cancellationToken);
        Task UpdateProjectAsync(ProjectViewModel project, CancellationToken cancellationToken);
        Task<IEnumerable<ProjectViewModel>> SubProjectsListAsync(int projectId, CancellationToken cancellationToken);
    }
}