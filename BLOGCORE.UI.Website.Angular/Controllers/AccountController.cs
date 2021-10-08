using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.APPLICATION.Core.Utilities;
using BLOGCORE.APPLICATION.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IConfiguration _configuration;

        public AccountController(IUsuarioService usuarioService, IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _usuarioRepositorio = usuarioRepositorio;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn(UsuarioSignInViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Ip = GetIp();
                    model.Password = Crypto.CifrarClave(model.Password);
                    var usr = _usuarioService.SignIn(model);
                    UsuarioLoginDto usuario;
                    if (usr.TieneError)
                    {
                        usuario = new UsuarioLoginDto
                        {
                            Errors = usr.MensajeLogin,
                            Autenticado = false
                        };
                        return BadRequest(usuario);
                    }

                    DateTime fechaActual = DateTime.UtcNow;
                    DateTime fechaExpiracion = fechaActual.AddMinutes(Utilities.Constants.ExpiracionMinutos);

                    var token = CreateToken(usr.Usuario, fechaExpiracion, fechaActual);

                    usuario = new UsuarioLoginDto
                    {
                        Id = usr.UsuarioId,
                        Apellidos = usr.Usuario.Perfil.Apellidos,
                        Nombres = usr.Usuario.Perfil.Nombres,
                        Email = usr.Usuario.Email,
                        Token = token,
                        Username = usr.Username,
                        RefreshToken = token,
                        Autenticado = true,
                        ExpiresIn = fechaExpiracion
                    };

                    return Ok(usuario);
                }

                return BadRequest(model);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> CurrentUser()
        {
            var usuario = await _usuarioRepositorio.CurrentUser(GetUsuarioSesion());
            DateTime fechaActual = DateTime.UtcNow;
            DateTime fechaExpiracion = fechaActual.AddMinutes(Utilities.Constants.ExpiracionMinutos);

            if (usuario != null)
            {
                var token = CreateToken(usuario, fechaExpiracion, fechaActual);

                var usuarioDto = new UsuarioLoginDto
                {
                    Id = usuario.Id,
                    Apellidos = usuario.Perfil.Apellidos,
                    Nombres = usuario.Perfil.Nombres,
                    Email = usuario.Email,
                    Token = token,
                    Username = usuario.Username,
                    RefreshToken = token,
                    Autenticado = true,
                    ExpiresIn = fechaExpiracion
                };
                return Ok(usuarioDto);
            }
            return BadRequest(new UsuarioDto { Autenticado = false, Errors = "No se encontro el usuario" });
        }
    }
}
