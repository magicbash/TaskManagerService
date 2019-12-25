using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagerService.Data;
using TaskManagerService.Data.Enitites;
using TaskManagerService.Data.UnitOfWork;
using Task = TaskManagerService.Data.Enitites.Task;

namespace TaskManagerService.Business
{
    public class ProjectStateCalculation : IProjectStateCalculator, IProjectStateUpdater
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectStateUpdater _projectStateUpdater;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProjectStateCalculation(IProjectRepository projectRepository, 
            ITaskRepository taskRepository, 
            IProjectStateUpdater projectStateUpdater, 
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _projectStateUpdater = projectStateUpdater;
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async System.Threading.Tasks.Task UpdateProjectStateAsync(int projectId, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(projectId, cancellationToken);

            var tasks = await _taskRepository.FindProjectTasksAsync(projectId, cancellationToken);
            var subProjects = await _projectRepository.FindSubProjectsAsync(projectId, cancellationToken);

            project.State = CalculateProjectState(tasks, subProjects);

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                await _projectRepository.UpdateAsync(project, cancellationToken);

                await unitOfWork.CompleteAsync(cancellationToken);
            }

            if (project.ProjectId.HasValue)
            {
                await _projectStateUpdater.UpdateProjectStateAsync(project.ProjectId.Value, cancellationToken);
            }
        }

        public State CalculateProjectState(IEnumerable<Task> subTasks, IEnumerable<Project> subProjects)
        {
            if (subTasks.Any(a=>a.State == State.InProgress) || subProjects.Any(a=>a.State == State.InProgress))
            {
                return State.InProgress;
            }

            if (subTasks.All(a=>a.State == State.Completed) && subProjects.Any(a=>a.State == State.InProgress))
            {
                return State.Completed;
            }

            return State.Planned;
        }
    }
}