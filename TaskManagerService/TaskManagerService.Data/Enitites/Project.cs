using System;
using System.Collections.Generic;

namespace TaskManagerService.Data.Enitites
{
    public class Project : IEntity<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public State State { get; set; }
        public int? ProjectId { get; set; }
        public IEnumerable<Project> SubProject { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}