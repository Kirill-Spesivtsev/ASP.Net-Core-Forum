using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace ForumProject.Services
{
    public class UploadService : IUpload
    {
        public CloudBlobContainer GetBlobContainer(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference("profile-images");
        }
    }
}
