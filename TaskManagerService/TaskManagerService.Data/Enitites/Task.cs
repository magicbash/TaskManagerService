using System;
using System.Collections.Generic;

namespace TaskManagerService.Data.Enitites
{
    public class Task : IEntity<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public State State { get; set; }
        public int? TaskId { get; set; }
        public int? ProjectId { get; set; }
        public IEnumerable<Task> SubTasks { get; set; }
    }
}