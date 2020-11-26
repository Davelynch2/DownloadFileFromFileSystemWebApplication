using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DownloadFileFromFileSystemWebApplication.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DownloadFileFromFileSystemWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        [HttpGet("{fileName}/data")]
        public FileStreamResult FinDocumentData(string fileName)
        {
            var filePath = FileHelper.GetFilePathFromListWithFileNameParameter(fileName);
            //var filePath = FileHelper.GetFilePath(fileName);
            var fileContent = System.IO.File.ReadAllBytes(filePath);
            var ms = new MemoryStream(fileContent);
            var fileType = "text/plain";
            var result = new FileStreamResult(ms, fileType);

            result.FileDownloadName = fileName + Path.GetExtension(filePath);

            return result;
        }
    }
}
