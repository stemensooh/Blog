using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var authenticateResult = (HttpContext.AuthenticateAsync().Result);
            return long.Parse(authenticateResult.Principal.Claims.First(x => x.Type.Equals("UsuarioId"))?.Value ?? "0");
        }
    }
}
