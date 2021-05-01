using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Data
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> GetUsuarioAsync(string Email, string Password);
        Task<List<Rol>> GetRolesAsync(int[] roles);
    }
}
