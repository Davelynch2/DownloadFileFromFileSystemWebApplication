using DownloadFileFromFileSystemWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.Repositories
{
    public interface IDocumentRepository
    {
        Task Create(DocumentUpload documentUpload);
        Task Save();
        Task<FileStreamResult> FindDocumentData(Guid id);
        Task<Document> FindDocument(Guid id);
    }
}
