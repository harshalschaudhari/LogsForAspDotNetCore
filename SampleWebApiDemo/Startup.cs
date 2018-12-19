using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.RollingFile;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace SampleWebApiDemo {
    public class Startup {
        public Startup(IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("configoverride/simpleapi.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();

            string esConnectionString = Convert.ToString(Configuration["Cloud:Log_ES_ENDPOINT"]);
            //Serilog             
            Helper.Serilogger.GetLoggerConfiguration(esConnectionString);
            //Write the log on console.
            System.Console.WriteLine("Console Log: Write log on console from Startup method.");

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

          

            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
