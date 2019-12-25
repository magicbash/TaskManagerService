using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManagerService.Data.BaseRepository;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Data
{
    public interface IProjectRepository : IBaseRepository<Project, int>
    {
        Task<IEnumerable<Project>> FilterByDateAndStateAsync(DateTime dateTime, 
            State state,
            CancellationToken cancellationToken);
        
        Task<IEnumerable<Project>> FindSubProjectsAsync(int projectId, CancellationToken cancellationToken);
    }
}