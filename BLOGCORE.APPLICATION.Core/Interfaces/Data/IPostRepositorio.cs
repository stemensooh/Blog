using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Data
{
    public interface IPostRepositorio
    {
        Task<List<Post>> GetPosts();
        Task<List<Post>> GetPosts(int Top);
        Task<List<Post>> GetPosts(long UsuarioId);
        Task<Post> GetPost(long PostId, long usuarioId, bool Pantalla, string Ip);
        Post GetPost(long PostId, long UsuarioId);
        Post AgregarPost(Post post);
        Post EditarPost(Post post);
        int EliminarPost(long PostId, long UsuarioId);
        List<PostVistas> GetVistas(long PostId);
        List<PostVistasAnonimas> GetVistasAnonima(long PostId);
        
    }
}
