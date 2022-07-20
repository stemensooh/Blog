using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Interfaces;
using GS.TOOLS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.API.POST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : BaseController
    {
        private readonly IComentarioService _comentarioService;
        private readonly IConfiguration _configuration;

        public ComentarioController(IComentarioService comentarioService, IConfiguration configuration)
        {
            _comentarioService = comentarioService;
            _configuration = configuration;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(long Id)
        {
            var result = await _comentarioService.GetComentariosPostId(Id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(ComentarioAddDto comentario)
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
                        EncodedResponse = comentario.Captcha
                    };

                    string mensaje = null;
                    var resultGoogle = GSRecaptchaGoogle.Validar(ref google, ref mensaje);

                    if (!resultGoogle)
                    {
                        result.Errors = new { ErrorGeneral = new string[] { "Error en el captcha" } };
                        return BadRequest(result);
                    }

                    comentario.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    if (await _comentarioService.Registrar(comentario))
                    {
                        result.Estado = true;
                    }
                    else
                    {
                        result.Mensaje = "Error al guardar el comentario";
                    }

                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
