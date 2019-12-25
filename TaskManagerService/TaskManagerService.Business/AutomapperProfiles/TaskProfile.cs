using AutoMapper;
using TaskManagerService.Data.Enitites;
using TaskManagerService.Data.Models;

namespace TaskManagerService.Business.AutomapperProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskViewModel>(MemberList.None);

            CreateMap<TaskViewModel, Task>(MemberList.None);
        }
    }
}