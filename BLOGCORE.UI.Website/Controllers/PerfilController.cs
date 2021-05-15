using BLOGCORE.APPLICATION.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    public class PerfilController : BaseController
    {
        private readonly IUsuarioService usuarioService;

        public PerfilController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index(string username)
        {
            bool EsAdmin = GetRol() == null ? false : (GetRol().Contains(BLOGCORE.APPLICATION.Core.Constants.Constantes.Rol.Administrador.ToString()) ? true : false);
            if (string.IsNullOrEmpty(username))
            {
                username = User.Identity.Name;
            }

            return View(await usuarioService.VerPerfilAsync(username, EsAdmin));
        }
    }
}
