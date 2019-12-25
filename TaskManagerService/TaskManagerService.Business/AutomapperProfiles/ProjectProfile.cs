using AutoMapper;
using TaskManagerService.Data.Enitites;
using TaskManagerService.Data.Models;

namespace TaskManagerService.Business.AutomapperProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectViewModel>(MemberList.None);

            CreateMap<ProjectViewModel, Project>(MemberList.None);
        }
    }
}