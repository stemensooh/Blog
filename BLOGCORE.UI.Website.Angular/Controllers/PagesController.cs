using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : BaseController
    {
        public IActionResult AccessDeniedPath() => Ok();
        
        public IActionResult Page404() => BadRequest();

        public IActionResult Index()
        {
            return Ok();
        }
    }
}
