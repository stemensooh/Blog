using BLOGCORE.APPLICATION.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> GetAll();
    }
}
