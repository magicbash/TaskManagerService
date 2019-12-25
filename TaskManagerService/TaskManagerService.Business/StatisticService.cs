using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using TaskManagerService.Data;
using TaskManagerService.Data.Enitites;
using TaskManagerService.Data.Models;

namespace TaskManagerService.Business
{
    public class StatisticService : IStatisticService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IStatisticReportGenerator<StatisticReportModel> _statisticReportGenerator;
        private readonly IMapper _mapper;

        public StatisticService(IProjectRepository projectRepository, 
            ITaskRepository taskRepository, 
            IStatisticReportGenerator<StatisticReportModel> statisticReportGenerator,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _statisticReportGenerator = statisticReportGenerator;
            _mapper = mapper;
        }
        public async Task<XLWorkbook> GetStatisticAsync(DateTime time, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.FilterByDateAndStateAsync(time, State.InProgress, cancellationToken);
            var tasks = await _taskRepository.FilterByDateAndStateAsync(time, State.InProgress, cancellationToken);

            return _statisticReportGenerator.GenerateReport(new StatisticReportModel
            {
                Projects = projects.Select(a => _mapper.Map<ProjectViewModel>(a)),
                Tasks = tasks.Select(a => _mapper.Map<TaskViewModel>(a))
            });
        }
    }
}