using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.CustomeExceptions
{
    public class FileCacheException : Exception
    {
        public FileCacheException(string msg) : base(msg)
        {
        }
    }
}
