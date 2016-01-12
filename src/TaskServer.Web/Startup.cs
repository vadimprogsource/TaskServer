using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskServer.Repository;
using TaskServer.Interfaces.Repository;
using TaskServer.Web.Security;
using TaskServer.Core;
using TaskServer.Interfaces.Services;
using TaskServer.Interfaces.Security;

namespace TaskServer.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IRepositoryFactory>(x => new RepositoryFactory(Configuration["Data:DefaultConnection:ConnectionString"]));
            services.AddScoped(x => x.GetRequiredService<IRepositoryFactory>().CreateRepository<IUserRepository>());
            services.AddScoped(x => x.GetRequiredService<IRepositoryFactory>().CreateRepository<ITaskRepository>());
            services.AddScoped(x => x.GetRequiredService<IRepositoryFactory>().CreateRepository<IClassifierRepository>());

            services.AddScoped<IClassifiersService, ClassifiersService>();
            services.AddScoped<ITaskWorkflowService, TaskWorkflowService>();


            services.AddScoped<IUserContext>(x => new UserContext(x.GetRequiredService<IUserRepository>()));


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
