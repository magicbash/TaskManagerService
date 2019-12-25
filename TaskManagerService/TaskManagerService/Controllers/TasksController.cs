using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagerService.Business;
using TaskManagerService.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }
        
        [HttpGet("{id}")]
        public Task<TaskViewModel> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _service.GetTaskAsync(id, cancellationToken);
        }
        
        [HttpDelete("{id}")]
        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return _service.DeleteTaskAsync(id, cancellationToken);
        }
        
        [HttpPost]
        public async Task CreateAsync(TaskViewModel task, CancellationToken cancellationToken)
        {
            await _service.CreateTaskAsync(task, cancellationToken);
        }
        
        [HttpPut]
        public Task UpdateAsync(TaskViewModel task, CancellationToken cancellationToken)
        {
            return _service.UpdateTaskAsync(task, cancellationToken);
        }
        
        [HttpGet]
        public Task<IEnumerable<TaskViewModel>> ListAsync(int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            return _service.ListAsync(pageSize, pageIndex, cancellationToken);
        }
        
        [HttpGet("{id}/subTasks")]
        public Task<IEnumerable<TaskViewModel>> SubTasksListAsync(int id, CancellationToken cancellationToken)
        {
            return _service.SubTasksListAsync(id, cancellationToken);
        }
    }
}