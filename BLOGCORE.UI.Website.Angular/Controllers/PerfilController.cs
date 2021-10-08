using BLOGCORE.APPLICATION.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{username}")]
        public  IActionResult Get(string username)
        {
            if (User.Identity.IsAuthenticated)
            {
                bool EsAdmin = GetRol() != null && (GetRol().Contains(BLOGCORE.APPLICATION.Core.Constants.Constantes.Rol.Administrador.ToString()));
                if (string.IsNullOrEmpty(username))
                {
                    username = User.Identity.Name;
                }

                return Ok( usuarioService.VerPerfil(username, EsAdmin));
            }
            return BadRequest();
        }
    }
}
