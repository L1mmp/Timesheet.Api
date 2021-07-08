using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Timesheet.Application.Services;
using Timesheet.Domain.Services;
using Timesheet.Domain.Repositories;
using Timesheet.DataAccess.CSV;

namespace Timesheet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITimesheetService, TimesheetService>();
            services.AddTransient<ITimesheetRepository, TimesheetRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IReportService, ReportService>();

            //"\\home\\user\\MyProjects\\C#_Projects\\Timesheet.Api\\Timesheet.DataAccess\\employees.csv";
            services.AddSingleton<CsvSettings>(x => new CsvSettings( "..\\Timesheet.DataAccess\\Data", ';'));
         
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
