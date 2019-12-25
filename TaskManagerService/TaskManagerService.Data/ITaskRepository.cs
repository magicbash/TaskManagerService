using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManagerService.Data.BaseRepository;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Data
{
    public interface ITaskRepository : IBaseRepository<TaskManagerService.Data.Enitites.Task, int>
    {
        Task<IEnumerable<Enitites.Task>> FilterByDateAndStateAsync(DateTime dateTime, 
            State state,
            CancellationToken cancellationToken);
        
        Task<IEnumerable<Enitites.Task>> FindSubTasksAsync(int taskId, CancellationToken cancellationToken);
        Task<IEnumerable<Enitites.Task>> FindProjectTasksAsync(int projectId, CancellationToken cancellationToken);
    }
}