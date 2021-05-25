using BLOGCORE.APPLICATION.Core.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Storage
{
    public interface IStorageService
    {
        Task<TaskResponseDto> GuardarImagen(MemoryStream ms, string Name, string containerName = null);
    }
}
