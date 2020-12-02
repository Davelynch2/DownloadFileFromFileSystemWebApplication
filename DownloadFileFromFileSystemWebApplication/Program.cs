using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadFileFromFileSystemWebApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DownloadFileFromFileSystemWebApplication
{
    public class Program
    {
        //"Server=jfgibeau01\\SQL2017;Database=DocumentFileSystemDJ; MultipleActiveResultSets=True;Trusted_Connection=True" : remote DB
        //"Server=(local)\\SQLEXPRESS;Database=DocumentFileSystem;Trusted_Connection=True" : local DB
        public static void Main(string[] args)
        {
            IHost host = null;

            try
            {
                host = CreateHostBuilder(args).Build();
                CreateDbIfNotExists(host);
            }
            catch
            {
                throw;
            }
            try
            {
                host.Run();
            }
            catch
            {
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseKestrel()
                    .UseStartup<Startup>();
                });
                

        private static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<DocumentContext>();
                context.EnsureDatabaseCreated();
            }
            catch
            {
                throw;
            }
        }
    }

}
