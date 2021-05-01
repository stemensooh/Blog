using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult AccessDeniedPath() => View();
        
        public IActionResult Page404() => View();

        public IActionResult Index()
        {
            return View();
        }
    }
}
