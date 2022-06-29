using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;

namespace ForumProject.Interfaces
{
    public interface IUpload
    {
        CloudBlobContainer GetBlobContainer(string connectionString);
    }
}
