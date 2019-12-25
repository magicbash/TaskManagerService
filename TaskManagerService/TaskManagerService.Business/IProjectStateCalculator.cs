using System.Collections.Generic;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Business
{
    public interface IProjectStateCalculator
    {
        State CalculateProjectState(IEnumerable<Task> subTasks,
            IEnumerable<Project> subProjects);
    }
}