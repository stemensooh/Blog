using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    [Authorize(Roles = "SuperAdministrador,Administrador")]
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
            return View();
        }

        public async Task<IActionResult> AccesosUsuario()
        {
            return View(await usuarioService.GetAccesosUsuarios());
        }

    }
}
