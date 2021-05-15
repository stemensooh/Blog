using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
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
    public class AccountController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AccountController(IUsuarioService usuarioService, IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioService = usuarioService;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        #region SignIn
        public IActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(UsuarioSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Password = Crypto.CifrarClave(model.Password);
                var usr = await _usuarioService.SignInAsync(model);
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

        #endregion

        #region SignUp
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(UsuarioSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usr = null;
                usr = await _usuarioRepositorio.GetUsuarioAsync(model.Username, null);
                if(usr != null)
                {
                    ModelState.AddModelError("Username", "Este nombre de usuario ya se encuentra registrado");
                    return View(model);
                }

                usr = await _usuarioRepositorio.GetUsuarioAsync(null, model.Email);
                if (usr != null)
                {
                    ModelState.AddModelError("Email", "Este correo electónico ya se encuentra registrado");
                    return View(model);
                }

                string password = model.Password;
                model.Password = Crypto.CifrarClave(model.Password);
                var result = await _usuarioService.SignUpAsync(model);
                if (result is null)
                {
                    ModelState.AddModelError(string.Empty, "Ha habido un error al crear el usuario");
                }

                return await SignInAsync(new UsuarioSignInViewModel() { Email = model.Email, Password = password });
            }

            return View(model);
        }
        #endregion


        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
