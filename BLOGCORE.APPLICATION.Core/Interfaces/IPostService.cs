using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDto>> GetPostsAsync();
        Task<List<PostDto>> GetPostsAsync(long UsuarioId);
        Task<PostDto> GetPostAsync(long PostId, long usuarioId, bool Pantalla, string Ip);
        Task<bool> AgregarPostAsync(PostViewModel model);
        Task<int> EliminarPostAsync(long PostId, long UsuarioId);
        Task<List<UsuarioDto>> GetVistasUsuario(long PostId, long UsuarioId);
        Task<List<UsuarioDto>> GetVistasAnonima(long PostId, long UsuarioId);
    }
}
