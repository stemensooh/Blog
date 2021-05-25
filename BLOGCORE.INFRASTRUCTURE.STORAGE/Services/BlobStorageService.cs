using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Interfaces.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.INFRASTRUCTURE.STORAGE
{
    public class BlobStorageService : IStorageService
    {
        private string _connectionString;
        private string _accountKey;
        private string _accountName;

        public BlobStorageService(string connectionString, string accountKey, string accountName)
        {
            _connectionString = connectionString;
            _accountKey = accountKey;
            _accountName = accountName;
        }

        public async Task<TaskResponseDto> GuardarImagen(MemoryStream ms, string Name, string containerName = null)
        {
            TaskResponseDto taskResponseDto = new TaskResponseDto();
            try
            {
                BlobContainerClient containerClient = new BlobContainerClient(_connectionString, containerName);
                BlobClient blobClient = containerClient.GetBlobClient(Name);
                BlobContentInfo info = await blobClient.UploadAsync(ms, true);
                taskResponseDto.Data = info;
            }
            catch (Exception ex)
            {
                taskResponseDto.TieneError = true;
            }

            return taskResponseDto;
        }


    }
}
