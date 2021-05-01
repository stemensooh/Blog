using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    public class UsuarioController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
