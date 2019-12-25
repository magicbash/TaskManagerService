using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using TaskManagerService.Data.BaseRepository;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Data
{
    public class TaskRepository : SqlRepositoryBase<Context, Task, int>, ITaskRepository
    {
        public TaskRepository(Context dbContext) : base(dbContext)
        {
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> FilterByDateAndStateAsync(DateTime dateTime, State state, CancellationToken cancellationToken)
        {
            var dateAfter = dateTime.Date;
            var dateBefore = dateTime.Date.AddDays(1);
            
            return await Entities.Where(a => a.State == state && dateAfter < a.StartDate && a.StartDate < dateBefore)
                .ToListAsync(cancellationToken);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> FindSubTasksAsync(int taskId, CancellationToken cancellationToken)
        {
            return await Entities.Where(a => a.TaskId == taskId).ToListAsync(cancellationToken);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> FindProjectTasksAsync(int projectId, CancellationToken cancellationToken)
        {
            return await Entities.Where(a => a.ProjectId == projectId).ToListAsync(cancellationToken);
        }
    }
}