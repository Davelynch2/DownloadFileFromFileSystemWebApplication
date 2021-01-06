using DownloadFileFromFileSystemWebApplication.CustomeExceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.Models
{
    public class FileCache
    {
        private readonly ConcurrentDictionary<Guid, string> _cache;
        public FileCache()
        {
            _cache = new ConcurrentDictionary<Guid, string>();
        }

        public bool TryAdd(Document entity) => !_cache.TryAdd(entity.Id, entity.Location) ? throw new FileCacheException("New Data was not added to the cache.") : true;

        public bool TryGetValue(Guid documentId, out string fileLocation) => _cache.TryGetValue(documentId, out fileLocation);
    }
}
