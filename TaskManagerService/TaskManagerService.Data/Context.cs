using Microsoft.EntityFrameworkCore;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        
        public Context() 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string cn = @"Server=(local);Database=TaskManager;User=sa;Password=password1@A";
            optionsBuilder.UseSqlServer(cn);
  
            base.OnConfiguring(optionsBuilder);
        }

        
    }
}