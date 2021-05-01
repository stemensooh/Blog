using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Utilities;
using BLOGCORE.APPLICATION.Core.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync(UsuarioLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Password = Crypto.CifrarClave(model.Password);
                var usr = await _usuarioService.LoginAsync(model);
                if (usr.TieneError)
                {
                    ModelState.AddModelError(string.Empty, usr.MensajeLogin);
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usr.Username),
                    new Claim(ClaimTypes.Role, usr.Rol),
                    new Claim("UsuarioId", usr.UsuarioId.ToString()),
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", ClaimTypes.Name, ClaimTypes.Role)));

                return RedirectToAction("Index", "Posts");

            }
            return View(model);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
