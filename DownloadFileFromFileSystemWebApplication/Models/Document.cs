using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.Models
{
    public class Document
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonPropertyName("id")]
        public Guid Id { get; }

        [Required]
        [JsonPropertyName("revision")]
        public int Revision { get; private set; }

        [Required(ErrorMessage = "The name attribute is missing.")]
        [StringLength(100, MinimumLength = 1)]
        [JsonPropertyName("name")]
        public string Name { get; private set; } = string.Empty;

        [StringLength(250, MinimumLength = 0)]
        [JsonPropertyName("location")]
        public string Location { get; private set; } = string.Empty;

        [Required(ErrorMessage = "The type attribute is missing.")]
        [StringLength(50, MinimumLength = 1)]
        [JsonPropertyName("fileType")]
        public string FileType { get; private set; } = string.Empty;

        [Required(ErrorMessage = "The size attribute is missing.")]
        [JsonPropertyName("size")]
        public long Size { get; private set; } = 0;

        //[Required(ErrorMessage = "The status attribute is missing.")]
        //[JsonPropertyName("status")]
        //public Status Status { get; set; }

        [Required(ErrorMessage = "The lastTimeModified attribute is missing.")]
        [JsonPropertyName("lastTimeModified")]
        public DateTime LastTimeModified { get; private set; } = DateTime.UtcNow;

        private string _location => @"D:\DocumentStorage";

        public Document() {}

        public Document(DocumentUpload documentUpload)
        {
            Id = Guid.NewGuid();
            Revision = 0;

            if (!string.IsNullOrEmpty(documentUpload.Name))
                Name = documentUpload.File.FileName.Split('.').First();

            if (documentUpload.File != null)
            {
                FileType = documentUpload.File.ContentType;
                Location = $@"{_location}\{documentUpload.File.FileName}";
                Size = documentUpload.File.Length;
            }

            //Status = Status.Create;
        }

    }
}
