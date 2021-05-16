using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.APPLICATION.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task<RespuestaLoginDto> SignInAsync(UsuarioSignInViewModel model);
        Task<PerfilDto> VerPerfilAsync(string username, bool EsAdministrador = false);
        Task<Usuario> SignUpAsync(UsuarioSignUpViewModel model);
        Task<List<AccesoUsuarioDto>> GetAccesosUsuarios();
    }
}
