using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Data
{
    public interface ICategoriaRepositorio
    {
        Task<List<Categoria>> GetAll();
        void GuardarDetalle(long Post, int Categoria);
        void LimpiarDetalleCategoria(long Post);
    }
}
