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
        List<Post> GetPosts(long UsuarioId);
        Post GetPost(long PostId, long usuarioId, bool Pantalla, string Ip);
        Post GetPost(long PostId, long UsuarioId);
        bool AgregarPost(Post post);
        bool EditarPost(Post post);
        int EliminarPost(long PostId, long UsuarioId);
        List<PostVistas> GetVistas(long PostId);
        List<PostVistasAnonimas> GetVistasAnonima(long PostId);
        
    }
}
