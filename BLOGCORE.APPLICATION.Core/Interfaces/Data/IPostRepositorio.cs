using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Data
{
    public interface IPostRepositorio
    {
        Task<List<Post>> GetPostsAsync();
        Task<List<Post>> GetPostsAsync(long UsuarioId);
        Task<Post> GetPostAsync(long PostId, long usuarioId, bool Pantalla);
        Task<Post> GetPostAsync(long PostId, long UsuarioId);
        Task<bool> AgregarPostAsync(Post post);
        Task<bool> EditarPostAsync(Post post);
        Task<int> EliminarPostAsync(long PostId, long UsuarioId);
        Task<List<PostVistas>> GetVistasAsync(long PostId);
    }
}
