using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagerService.Business;
using TaskManagerService.Data.Enitites;
using TaskManagerService.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }
        
        [HttpGet("{id}")]
        public Task<ProjectViewModel> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _service.GetProjectAsync(id, cancellationToken);
        }
        
        [HttpDelete("{id}")]
        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return _service.DeleteProjectAsync(id, cancellationToken);
        }
        
        [HttpPost]
        public Task CreateAsync(ProjectViewModel project, CancellationToken cancellationToken)
        {
            return _service.CreateProjectAsync(project, cancellationToken);
        }
        
        [HttpPut]
        public Task UpdateAsync(ProjectViewModel project, CancellationToken cancellationToken)
        {
            return _service.UpdateProjectAsync(project, cancellationToken);
        }
        
        [HttpGet]
        public Task<IEnumerable<ProjectViewModel>> ListAsync(int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            return _service.ListAsync(pageSize, pageIndex, cancellationToken);
        }
        
        [HttpGet("{id}/subProjects")]
        public Task<IEnumerable<ProjectViewModel>> SubProjectsListAsync(int id, CancellationToken cancellationToken)
        {
            return _service.SubProjectsListAsync(id, cancellationToken);
        }
    }
}