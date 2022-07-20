using BLOGCORE.APPLICATION.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : BaseController
    {
        private readonly IUsuarioService usuarioService;

        public PerfilController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [Authorize]
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
    }
}
