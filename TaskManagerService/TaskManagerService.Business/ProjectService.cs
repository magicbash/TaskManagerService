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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IProjectStateUpdater _projectStateUpdater;

        public ProjectService(IProjectRepository projectRepository, 
            IMapper mapper, 
            IUnitOfWorkFactory unitOfWorkFactory, 
            IProjectStateUpdater projectStateUpdater)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _unitOfWorkFactory = unitOfWorkFactory;
            _projectStateUpdater = projectStateUpdater;
        }
        public async Task<ProjectViewModel> GetProjectAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetAsync(id, cancellationToken);
            return _mapper.Map<ProjectViewModel>(result);
        }

        public async Task<IEnumerable<ProjectViewModel>> ListAsync(int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.ListAsync(pageSize, pageIndex, cancellationToken);
            return result.Select(a => _mapper.Map<ProjectViewModel>(a));
        }

        public async Task DeleteProjectAsync(int id, CancellationToken cancellationToken)
        {
            await _projectRepository.DeleteAsync(id, cancellationToken);
            await _projectStateUpdater.UpdateProjectStateAsync(id, cancellationToken);
        }

        public async Task CreateProjectAsync(ProjectViewModel project, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Project>(project);
            
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                await _projectRepository.AddAsync(entity, cancellationToken);
                
                await unitOfWork.CompleteAsync(cancellationToken);
            }
            
            await _projectStateUpdater.UpdateProjectStateAsync(entity.Id, cancellationToken);
        }

        public async Task UpdateProjectAsync(ProjectViewModel project, CancellationToken cancellationToken)
        {
            var entity = await _projectRepository.GetAsync(project.Id, cancellationToken);
            _mapper.Map(project, entity);
            
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                await _projectRepository.UpdateAsync(entity, cancellationToken);
                
                await unitOfWork.CompleteAsync(cancellationToken);
            }
            
            await _projectStateUpdater.UpdateProjectStateAsync(entity.Id, cancellationToken);
        }

        public async Task<IEnumerable<ProjectViewModel>> SubProjectsListAsync(int projectId, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.FindSubProjectsAsync(projectId, cancellationToken);
            
            return result.Select(a => _mapper.Map<ProjectViewModel>(a));
        }
    }
}