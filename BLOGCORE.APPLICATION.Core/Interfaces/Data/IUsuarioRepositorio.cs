using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Data
{
    public interface IUsuarioRepositorio
    {
        Usuario SignIn(string Email, string Password, string Ip);
        Usuario GetUsuario(string username, string email);
        List<Rol> GetRoles(int[] roles);
        Rol GetRol(string rol);
        int AddUsuario(Usuario usuario);
        int AddRol(Rol rol);
        int AddPerfil(Perfil perfil);
        List<AccesoUsuario> GetAccesoUsuarios();
    }
}
