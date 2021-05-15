using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task<RespuestaLoginDto> LoginAsync(UsuarioLoginViewModel model);
        Task<PerfilDto> VerPerfilAsync(string username, bool EsAdministrador = false);
    }
}
