using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.APPLICATION.Core.Utilities;
using BLOGCORE.APPLICATION.Core.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IConfiguration configuration;

        public AccountController(IUsuarioService usuarioService, IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _usuarioRepositorio = usuarioRepositorio;
            this.configuration = configuration;
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
        public  IActionResult SignIn(UsuarioSignInViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Ip = GetIp();
                    model.Password = Crypto.CifrarClave(model.Password);
                    var usr = _usuarioService.SignIn(model);
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

                     HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", ClaimTypes.Name, ClaimTypes.Role)));

                    return RedirectToAction("Index", "Posts");

                }
            }
            catch (Exception ex)
            {

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
        public  IActionResult SignUp(UsuarioSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usr = null;
                usr =  _usuarioRepositorio.GetUsuario(model.Username, null);
                if(usr != null)
                {
                    ModelState.AddModelError("Username", "Este nombre de usuario ya se encuentra registrado");
                    return View(model);
                }

                usr =  _usuarioRepositorio.GetUsuario(null, model.Email);
                if (usr != null)
                {
                    ModelState.AddModelError("Email", "Este correo electónico ya se encuentra registrado");
                    return View(model);
                }

                string password = model.Password;
                model.Password = Crypto.CifrarClave(model.Password);
                var result =  _usuarioService.SignUp(model);
                if (result is null)
                {
                    ModelState.AddModelError(string.Empty, "Ha habido un error al crear el usuario");
                }

                return SignIn(new UsuarioSignInViewModel() { Email = model.Email, Password = password });
            }

            return View(model);
        }
        #endregion


        public  IActionResult SignOut()
        {
             HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
