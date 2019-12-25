using System.Collections.Generic;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Data.Models
{
    public class StatisticReportModel
    {
        public IEnumerable<TaskViewModel> Tasks { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}