using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Angular.Controllers
{
    [Authorize(Roles = "SuperAdministrador,Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IUsuarioService usuarioService)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult AccesosUsuario()
        {
            return Ok( usuarioService.GetAccesosUsuarios());
        }

    }
}
