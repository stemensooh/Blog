using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.DomainServices
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaService(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }
        public async Task<List<CategoriaDto>> GetAll()
        {
            var result = await _categoriaRepositorio.GetAll();
            return result.Select(c => new CategoriaDto(c)).ToList();
        }
    }
}
