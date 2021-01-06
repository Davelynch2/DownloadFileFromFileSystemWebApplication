using DownloadFileFromFileSystemWebApplication.Models;
using DownloadFileFromFileSystemWebApplication.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.Extension
{
    static public class ServiceExtension
    {
        public static void AddDependency(this IServiceCollection services)
        {
            services.AddSingleton<FileCache>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
        }
    }
}
