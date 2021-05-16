using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.ViewModels;
using BLOGCORE.UI.Website.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Controllers
{
    [Authorize]
    public class PostsController : BaseController
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostService _postService;

        public PostsController(ILogger<PostsController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> IndexAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            TempData["UrlSearch"] = Url.Action("Index", "Posts");
            var posts = await _postService.GetPostsAsync();
            ViewBag.CantidadPosts = posts != null && posts.Any() ? posts.Count() : 0 ;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Titulo.ToLower().Contains(searchString.ToLower()) || s.Autor.ToLower().Contains(searchString.ToLower())).ToList();

            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    posts = posts.OrderByDescending(s => s.Titulo).ToList();
                    break;
                case "Date":
                    posts = posts.OrderBy(s => s.FechaCreacion).ToList();
                    break;
                case "date_desc":
                    posts = posts.OrderByDescending(s => s.FechaCreacion).ToList();
                    break;
                default:
                    posts = posts.OrderBy(s => s.Titulo).ToList();
                    break;
            }

            int pageSize = 6;
            return View(await PaginatedList<PostDto>.Create(posts.ToList(), pageNumber ?? 1, pageSize));

            //return View(posts);
        }

        public async Task<IActionResult> RegistrarAsync(long ID)
        {
            PostViewModel model = new PostViewModel();
            var post = await _postService.GetPostAsync(ID, GetUsuarioId(), false, "");
            if (post != null)
            {
                model.ID = post.ID;
                model.Titulo = post.Titulo;
                model.Cuerpo = post.Cuerpo;
                model.Imagen = post.Imagen;
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> VerPostAsync(long ID)
        {
            bool pantalla = false;
            long usuarioId = 0;

            if (User.Identity.IsAuthenticated)
            {
                usuarioId = GetUsuarioId();
                pantalla = true;
            }

            return View(await _postService.GetPostAsync(ID, usuarioId, pantalla, GetIp()));
        }

        [HttpPost]
        public async Task<IActionResult> EliminarPostAsync(long ID)
        {
            var result = await _postService.EliminarPostAsync(ID, GetUsuarioId());
            switch (result)
            {
                case 1:
                    return Json(new ResultViewDto() { Estado = false, Mensaje = "Registro inexistente" });
                case 2:
                    return Forbid();
                case 3:
                    return Json(new ResultViewDto() { Estado = true, Mensaje = "Registro eliminado" });
                default:
                    return Json(new ResultViewDto() { Estado = false, Mensaje = "Registro inexistente" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAsync(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UsuarioId = GetUsuarioId();
                var result = await _postService.AgregarPostAsync(model);
                if (result)
                {
                    return RedirectToAction("MisPosts");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se ha podido actualizar el registro correctamente");
                }
               
            }
            return View(model);
        }

        public async Task<IActionResult> MisPostsAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            TempData["UrlSearch"] = Url.Action("MisPosts", "Posts");
            var posts = await _postService.GetPostsAsync(GetUsuarioId());
            ViewBag.CantidadPosts = posts != null && posts.Any() ? posts.Count() : 0;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Titulo.ToLower().Contains(searchString.ToLower()) || s.Autor.ToLower().Contains(searchString.ToLower())).ToList();
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    posts = posts.OrderByDescending(s => s.Titulo).ToList();
                    break;
                case "Date":
                    posts = posts.OrderBy(s => s.FechaCreacion).ToList();
                    break;
                case "date_desc":
                    posts = posts.OrderByDescending(s => s.FechaCreacion).ToList();
                    break;
                default:
                    posts = posts.OrderBy(s => s.Titulo).ToList();
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<PostDto>.Create(posts.ToList(), pageNumber ?? 1, pageSize));

            //return View(posts);
        }

        [Authorize(Roles = "SuperAdministrador,Administrador")]
        public async Task<IActionResult> VistasUsuario(long Id)
        {
            return View(await _postService.GetVistasUsuario(Id, GetUsuarioId()));
        }

        [Authorize(Roles = "SuperAdministrador,Administrador")]
        public async Task<IActionResult> VistasAnonimas(long Id)
        {
            return View(await _postService.GetVistasAnonima(Id, GetUsuarioId()));
        }
    }
}
