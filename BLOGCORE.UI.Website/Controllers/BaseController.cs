using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            
        }

        public long GetUsuarioId()
        {
            var authenticateResult = HttpContext.AuthenticateAsync().Result;
            return long.Parse(authenticateResult.Principal.Claims.First(x => x.Type.Equals("UsuarioId"))?.Value ?? "0");
        }
        
        public string GetRol()
        {
            var authenticateResult = HttpContext.AuthenticateAsync().Result;
            return authenticateResult.Principal.Claims.First(x => x.Type.Equals(ClaimTypes.Role))?.Value?? null;
        }

        public string GetClaim(string claim)
        {
            var authenticateResult = (HttpContext.AuthenticateAsync().Result);
            return authenticateResult.Principal.Claims.First(x => x.Type.Equals(claim))?.Value ?? "";
        }
    }
}
