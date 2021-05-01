using BLOGCORE.APPLICATION.Core.Constants;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.UI.Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _postService.GetPostsAsync());
        }

        [Authorize(Roles = "SuperAdministrador,Administrador")]
        public IActionResult Privacy() => View();

        [Authorize]
        public async Task<IActionResult> UsuarioAsync()
        {
            return View(await _postService.GetPostsAsync(GetUsuarioId()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
