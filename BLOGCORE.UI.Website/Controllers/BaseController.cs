using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public string GetIp()
        {
            string localIP = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());// objeto para guardar la ip
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();// esta es nuestra ip
                }
            }

            return localIP;
        }

        public string GetClaim(string claim)
        {
            var authenticateResult = (HttpContext.AuthenticateAsync().Result);
            return authenticateResult.Principal.Claims.First(x => x.Type.Equals(claim))?.Value ?? "";
        }
    }
}
