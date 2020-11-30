using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DownloadFileFromFileSystemWebApplication.Helper;
using DownloadFileFromFileSystemWebApplication.Models;
using DownloadFileFromFileSystemWebApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DownloadFileFromFileSystemWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _repository;
        public DocumentController(IDocumentRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet("{fileName}/data")]
        //public FileStreamResult FinDocumentDataByName(string fileName)//just a test
        //{
        //    var filePath = FileHelper.GetFilePathFromListWithFileNameParameter(fileName);
        //    //var filePath = FileHelper.GetFilePath(fileName);
        //    var fileContent = System.IO.File.ReadAllBytes(filePath);
        //    var ms = new MemoryStream(fileContent);
        //    var fileType = "text/plain";
        //    var result = new FileStreamResult(ms, fileType);

        //    result.FileDownloadName = fileName + Path.GetExtension(filePath);

        //    return result;
        //}

        [HttpPost]
        //[RequestSizeLimit(104857600)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateDocument([FromForm] DocumentUpload documentUpload)
        {
            await _repository.Create(documentUpload);
            return Ok();
        }

        [HttpGet("{id}/data")]
        public async Task<FileStreamResult> FindDocumentData([Required] Guid id)
        {
            return await _repository.FindDocumentData(id);
        }
    }
}
