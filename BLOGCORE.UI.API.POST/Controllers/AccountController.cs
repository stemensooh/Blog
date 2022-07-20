using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.APPLICATION.Core.Utilities;
using BLOGCORE.APPLICATION.Core.ViewModels;
using GS.TOOLS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BLOGCORE.UI.API.POST.Controllers
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

        public IActionResult Get()
        {
            return Ok("Running...");
        }

        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn(UsuarioSignInViewModel model)
        {
            try
            {
                ResultDto result = new ResultDto();

                if (ModelState.IsValid)
                {
                    var google = new GSReCaptchaGoogleDto
                    {
                        ReCaptchaClaveSitioWeb = _configuration["Recaptcha:ClaveSitioWeb"],
                        ReCaptchaClaveComGoogle = _configuration["Recaptcha:ClaveComGoogle"],
                        ValorReCaptcha = null,
                        ReCaptchaCont = 0,
                        EncodedResponse = model.Captcha
                    };

                    string mensaje = null;
                    var resultGoogle = GSRecaptchaGoogle.Validar(ref google, ref mensaje);

                    if (!resultGoogle)
                    {
                        result.Errors = new { ErrorGeneral = new string[] { "Error en el captcha" } };
                        return BadRequest(result);
                    }

                    model.Ip = GetIp();
                    model.Password = Crypto.CifrarClave(model.Password);
                    var usr = _usuarioService.SignIn(model);
                    UsuarioLoginDto usuario;
                    if (usr.TieneError)
                    {
                        result.Errors = new { MensajeLogin = new string[] { usr.MensajeLogin } };
                        //result.Mensaje = usr.MensajeLogin;
                        return BadRequest(result);
                    }

                    var token = GenerarToken(usr.Usuario);
                    if (token != null)
                    {
                        result.Estado = true;
                    }

                    result.Data = new UsuarioLoginDto
                    {
                        Id = usr.UsuarioId,
                        Apellidos = usr.Usuario.Perfil.Apellidos,
                        Nombres = usr.Usuario.Perfil.Nombres,
                        Email = usr.Usuario.Email,
                        Token = token.Valor,
                        Username = usr.Username,
                        RefreshToken = token.Valor,
                        Autenticado = true,
                        ExpiresIn = token.Expira
                    };

                    return Ok(result);
                }
                else
                {
                    result.Errors = new { MensajeLogin = new string[] { "Modelo no válido" } };
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("Validate")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> CurrentUser()
        {
            try
            {
                ResultDto result = new ResultDto();

                var usuario = await _usuarioRepositorio.CurrentUser(GetUsuarioSesion());
                DateTime fechaActual = DateTime.UtcNow;
                DateTime fechaExpiracion = fechaActual.AddMinutes(Utilities.Constants.ExpiracionMinutos);

                if (usuario != null)
                {
                    var token = CreateToken(usuario);

                    result.Estado = true;
                    result.Data = new UsuarioLoginDto
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
                    return Ok(result);
                }
                else
                {
                    result.Mensaje = "No se encontro el usuario";
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }
}
