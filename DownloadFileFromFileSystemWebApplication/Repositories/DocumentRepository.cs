using DownloadFileFromFileSystemWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentContext _context;
        private readonly FileCache _fileCache;

        public DocumentRepository(DocumentContext documentContext, FileCache cache)
        {
            _context = documentContext;
            _fileCache = cache;
        }

        public async Task Create(DocumentUpload documentUpload)
        {
            var file = documentUpload.GetByteArrayData();
            var document = new Document(documentUpload);

            _fileCache.TryAdd(document);
            WriteFile(document.Location, file);

            _context.Documents.Add(document);
            await Save();
        }

        private void WriteFile(string location, byte[] file) => File.WriteAllBytes(location, file);

        public Task Save() => _context.SaveChangesAsync();

        public async Task<FileStreamResult> FindDocumentData(Guid id)
        {
            var document = await _context.Documents.Where(d => d.Id == id).FirstOrDefaultAsync();
            return GetDocumentData(document);
        }

        //private Document TryGetValueOrAddValue(Guid id)
        //{
        //    return !_fileCache.TryGetValue(id, out var fileLocation) ?
        //}
        //private string AddValueFromDbToCache(Guid id)
        //{

        //}

        // Read document document location
        private FileStreamResult GetDocumentData(Document document)
        {
            var fileContent = File.ReadAllBytes($@"{document.Location}");
            var ms = new MemoryStream(fileContent);
            var result = new FileStreamResult(ms, document.FileType);

            result.FileDownloadName = document.Name + Path.GetExtension($@"{document.Location}");

            return result;
        }

        public async Task<Document> FindDocument(Guid id)
        {
            return await _context.Documents.Where(d => d.Id == id).FirstOrDefaultAsync();
        }


    }

 
 
}
