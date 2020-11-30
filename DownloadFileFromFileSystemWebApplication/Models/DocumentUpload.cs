using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.Models
{
    public class DocumentUpload
    {
        [Required(ErrorMessage = "The name attribute is missing.")]
        [StringLength(100, MinimumLength = 1)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("file")]
        public IFormFile File { get; set; }

        public byte[] GetByteArrayData()
        {
            using var target = new MemoryStream();
            File.OpenReadStream().CopyTo(target);
            return target.ToArray();
        }

    }
}
