using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Data
{
    public interface IComentarioRepositorio
    {
        Task<List<Comentario>> GetComentariosPostId(long Id);
        Task<int> Add(Comentario comentario);
    }
}
