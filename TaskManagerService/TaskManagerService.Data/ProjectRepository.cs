using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerService.Data.BaseRepository;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Data
{
    public class ProjectRepository : SqlRepositoryBase<Context, Project, int>, IProjectRepository
    {
        public ProjectRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Project>> FilterByDateAndStateAsync(DateTime dateTime, State state, CancellationToken cancellationToken)
        {
            var dateAfter = dateTime.Date;
            var dateBefore = dateTime.Date.AddDays(1);
            
            return await Entities.Where(a => a.State == state && dateAfter < a.StartDate && a.StartDate < dateBefore)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Project>> FindSubProjectsAsync(int projectId, CancellationToken cancellationToken)
        {
            return await Entities.Where(a => a.ProjectId == projectId).ToListAsync(cancellationToken);
        }
    }
}