using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Data
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> SignInAsync(string Email, string Password);
        Task<Usuario> GetUsuarioAsync(string username, string email);
        Task<List<Rol>> GetRolesAsync(int[] roles);
        Task<Rol> GetRolAsync(string rol);
        Task<int> AddUsuario(Usuario usuario);
        Task<int> AddRol(Rol rol);
        Task<int> AddPerfil(Perfil perfil);
    }
}
