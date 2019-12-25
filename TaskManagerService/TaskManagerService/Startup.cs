using System;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TaskManagerService.Business;
using TaskManagerService.Data;
using TaskManagerService.Data.Models;
using TaskManagerService.Data.UnitOfWork;

namespace TaskManagerService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());;
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
            });
            
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IDataTableConverter, DataTableConverter>();
            services.AddTransient<IStatisticReportGenerator<StatisticReportModel>, ExcelReportGenerator>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddTransient<IProjectStateCalculator, ProjectStateCalculation>();
            services.AddTransient<IProjectStateUpdater, ProjectStateCalculation>();
            services.AddTransient<IUnitOfWorkFactory, EntityUnitOfWorkFactory>();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddDbContext<Context>(a =>
                a.UseSqlServer(Configuration.GetConnectionString(ConnectionStrings.MsSql)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Management API");
            });
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}