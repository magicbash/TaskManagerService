using System;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Data.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public State State { get; set; }
        public int? ProjectId { get; set; }
    }
}