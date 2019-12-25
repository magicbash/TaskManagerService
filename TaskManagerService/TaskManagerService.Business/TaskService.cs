using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagerService.Data;
using TaskManagerService.Data.Enitites;
using TaskManagerService.Data.Models;
using TaskManagerService.Data.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace TaskManagerService.Business
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IProjectStateUpdater _projectStateUpdater;

        public TaskService(ITaskRepository taskRepository, 
            IMapper mapper, 
            IUnitOfWorkFactory unitOfWorkFactory, 
            IProjectStateUpdater projectStateUpdater)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _unitOfWorkFactory = unitOfWorkFactory;
            _projectStateUpdater = projectStateUpdater;
        }
        
        public async Task<TaskViewModel> GetTaskAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _taskRepository.GetAsync(id, cancellationToken);
            return _mapper.Map<TaskViewModel>(result);
        }

        public async Task<IEnumerable<TaskViewModel>> ListAsync(int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            var result = await _taskRepository.ListAsync(pageSize, pageIndex, cancellationToken);
            return result.Select(a => _mapper.Map<TaskViewModel>(a));
        }

        public async Task DeleteTaskAsync(int id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetAsync(id, cancellationToken);
            
            await _taskRepository.DeleteAsync(id, cancellationToken);

            if (task.ProjectId.HasValue)
            {
                await _projectStateUpdater.UpdateProjectStateAsync(task.ProjectId.Value, cancellationToken);
            }
        }

        public async Task CreateTaskAsync(TaskViewModel task, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TaskManagerService.Data.Enitites.Task>(task);
            
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                await _taskRepository.AddAsync(entity, cancellationToken);
                
                await unitOfWork.CompleteAsync(cancellationToken);
            }
            
            if (entity.ProjectId.HasValue)
            {
                await _projectStateUpdater.UpdateProjectStateAsync(entity.ProjectId.Value, cancellationToken);
            }
        }

        public async Task UpdateTaskAsync(TaskViewModel task, CancellationToken cancellationToken)
        {
            var entity = await _taskRepository.GetAsync(task.Id, cancellationToken);
            _mapper.Map(task, entity);
            
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                await _taskRepository.UpdateAsync(entity, cancellationToken);

                await unitOfWork.CompleteAsync(cancellationToken);
            }
            
            if (entity.ProjectId.HasValue)
            {
                await _projectStateUpdater.UpdateProjectStateAsync(entity.ProjectId.Value, cancellationToken);
            }
        }

        public async Task<IEnumerable<TaskViewModel>> SubTasksListAsync(int taskId, CancellationToken cancellationToken)
        {
            var result = await _taskRepository.FindSubTasksAsync(taskId, cancellationToken);
            
            return result.Select(a => _mapper.Map<TaskViewModel>(a));
        }
    }
}