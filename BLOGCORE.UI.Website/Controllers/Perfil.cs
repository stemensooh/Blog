using BLOGCORE.APPLICATION.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    public class Perfil : BaseController
    {
        private readonly IUsuarioService usuarioService;

        public Perfil(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = User.Identity.Name;
            }

            return View(await usuarioService.VerPerfilAsync(username));
        }
    }
}
