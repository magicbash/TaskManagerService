using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManagerService.Data.Models;

namespace TaskManagerService.Business
{
    public interface ITaskService
    {
        Task<TaskViewModel> GetTaskAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<TaskViewModel>> ListAsync(int pageSize, int pageIndex, CancellationToken cancellationToken);
        Task DeleteTaskAsync(int id, CancellationToken cancellationToken);
        Task CreateTaskAsync(TaskViewModel task, CancellationToken cancellationToken);
        
        Task UpdateTaskAsync(TaskViewModel task, CancellationToken cancellationToken);
        Task<IEnumerable<TaskViewModel>> SubTasksListAsync(int taskId, CancellationToken cancellationToken);
    }
}