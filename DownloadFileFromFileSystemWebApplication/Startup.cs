using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadFileFromFileSystemWebApplication.Extension;
using DownloadFileFromFileSystemWebApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace DownloadFileFromFileSystemWebApplication
{
    public class Startup
    {
        private static ApiConfiguration _apiConfiguration;
        public Startup(IConfiguration configuration)
        {
            _apiConfiguration = new ApiConfiguration(configuration);
            //Configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(ApiConfiguration), _apiConfiguration);
            services.AddDbContext<DocumentContext>(
            options => options.UseMySQL(_apiConfiguration.SqlConnection));

            //services.AddDbContext<DocumentContext>(options => options.UseSqlServer(_apiConfiguration.SqlConnection));


            services.AddDependency();
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
