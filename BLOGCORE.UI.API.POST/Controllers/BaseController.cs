using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.UI.API.POST.Parameters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.UI.API.POST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private const string ServiceName = "SignIn";



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

        protected string CreateToken(Usuario usuario)
        {   try
            {
                var urlAuth = Request.GetDisplayUrl();
                var i = urlAuth.ToLower().IndexOf($"/{"validate".ToLower()}"); urlAuth = urlAuth.Remove(i);
                if (urlAuth.EndsWith("/")) urlAuth = urlAuth.Remove(urlAuth.Length - 1);

                var Expedido = DateTime.UtcNow;
                var Expira = Expedido.AddMinutes(ApiParameters.TokenDuracion);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Perfil.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Roles.FirstOrDefault().RolNavigation.Nombre),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("Id", usuario.Id.ToString()),
                    new Claim("Nombre", usuario.Perfil.Nombres),
                    new Claim("Apellido", usuario.Perfil.Apellidos),
                    //new Claim("FechaActual", Expedido.ToString()),
                    //new Claim("FechaExpiracion", Expira.ToString()),
                };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiParameters.TokenClave));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    NotBefore = Expedido,
                    Expires = Expira,
                    Issuer = urlAuth.ToLower(),
                    Audience = ApiParameters.TokenAudience, //ServicioEDOC.EnumtoString(),
                    SigningCredentials = creds
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

        protected Token GenerarToken(Usuario usuario)
        {
            try
            {
                var urlAuth = Request.GetDisplayUrl();
                var i = urlAuth.ToLower().IndexOf($"/{ServiceName.ToLower()}"); urlAuth = urlAuth.Remove(i);
                if (urlAuth.EndsWith("/")) urlAuth = urlAuth.Remove(urlAuth.Length - 1);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Perfil.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Roles.FirstOrDefault().RolNavigation.Nombre),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("Id", usuario.Id.ToString()),
                    new Claim("Nombre", usuario.Perfil.Nombres),
                    new Claim("Apellido", usuario.Perfil.Apellidos),
                    //new Claim("FechaActual", fechaActual.ToString()),
                    //new Claim("FechaExpiracion", fechaExpiracion.ToString()),
                };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiParameters.TokenClave));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var Expedido = DateTime.UtcNow;
                var Expira = Expedido.AddMinutes(ApiParameters.TokenDuracion);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    NotBefore = Expedido,
                    Expires = Expira,
                    Issuer = urlAuth.ToLower(),
                    Audience = ApiParameters.TokenAudience, //ServicioEDOC.EnumtoString(),
                    SigningCredentials = creds
                };
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var tokenValido = new Token()
                {
                    Valor = tokenHandler.WriteToken(securityToken),
                    Expedido = Expedido,
                    Expira = Expira
                };

                return tokenValido;
            }
            catch (Exception ex)
            {
                return new Token(ex.Message);
            }
        }


        protected string GetUsuarioSesion()
        {
            return HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

    }
}
