using BLOGCORE.APPLICATION.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected long GetUsuarioId()
        {
            var authenticateResult = HttpContext.AuthenticateAsync().Result;
            return long.Parse(authenticateResult.Principal.Claims.First(x => x.Type.Equals("Id"))?.Value ?? "0");
        }

        protected string GetRol()
        {
            try
            {
                var authenticateResult = HttpContext.AuthenticateAsync().Result;
                return authenticateResult.Principal.Claims.First(x => x.Type.Equals(ClaimTypes.Role))?.Value ?? null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected string GetIp()
        {
            return Request.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        protected string GetClaim(string claim)
        {
            var authenticateResult = (HttpContext.AuthenticateAsync().Result);
            return authenticateResult.Principal.Claims.First(x => x.Type.Equals(claim))?.Value ?? "";
        }

        protected string CreateToken(Usuario usuario, DateTime fechaExpiracion, DateTime fechaActual)
        {   try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Perfil.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Roles.FirstOrDefault().RolNavigation.Nombre),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("Id", usuario.Id.ToString()),
                    new Claim("Nombre", usuario.Perfil.Nombres),
                    new Claim("Apellido", usuario.Perfil.Apellidos),
                    new Claim("FechaActual", fechaActual.ToString()),
                    new Claim("FechaExpiracion", fechaExpiracion.ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C7q3FBCJZq0bIRRH0Dq4lxWuBipEBkHX"));
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = fechaExpiracion,
                    SigningCredentials = credential
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescription);

                HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", ClaimTypes.Name, ClaimTypes.Role)));

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected string GetUsuarioSesion()
        {
            return HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

    }
}
