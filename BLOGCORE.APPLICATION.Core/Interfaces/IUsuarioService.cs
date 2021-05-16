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
        RespuestaLoginDto SignIn(UsuarioSignInViewModel model);
        PerfilDto VerPerfil(string username, bool EsAdministrador = false);
        Usuario SignUp(UsuarioSignUpViewModel model);
        List<AccesoUsuarioDto> GetAccesosUsuarios();
    }
}
