using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLOGCORE.UI.API.POST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : BaseController
    {
        private readonly IUsuarioService usuarioService;
        private readonly IPerfilRepositorio perfilRepositorio;

        public PerfilController(IUsuarioService usuarioService, IPerfilRepositorio perfilRepositorio)
        {
            this.usuarioService = usuarioService;
            this.perfilRepositorio = perfilRepositorio;
        }

        public  IActionResult Get()
        {
            string username = GetClaim(ClaimTypes.Name);
            bool EsAdmin = GetRol() != null && (GetRol().Contains(BLOGCORE.APPLICATION.Core.Constants.Constantes.Rol.Administrador.ToString()));
            if (string.IsNullOrEmpty(username))
            {
                username = User.Identity.Name;
            }

            return Ok(usuarioService.VerPerfil(username, EsAdmin));
        }

        [AllowAnonymous]
        [HttpGet("VerPerfil/{username}")]
        public IActionResult VerPerfil(string username)
        {
            bool esAdmin = false;
            if (User.Identity.IsAuthenticated)
            {
                esAdmin = GetRol() != null && (GetRol().Contains(BLOGCORE.APPLICATION.Core.Constants.Constantes.Rol.Administrador.ToString()));
            }

            return Ok(usuarioService.VerPerfil(username, esAdmin));
        }

        [HttpGet("TipoRedSocial")]
        public IActionResult GetTipoRedSocial()
        {
            return Ok(perfilRepositorio.GetTipoRedSocial());
        }

        [AllowAnonymous]
        [HttpGet("MisRedes/{username}")]
        public IActionResult MisRedes(string username)
        {
            var result = perfilRepositorio.GetMisRedes(username);
            result.ForEach(x =>
            {
                x.Perfil = null;
            });
            return Ok(result);
        }



    }
}
