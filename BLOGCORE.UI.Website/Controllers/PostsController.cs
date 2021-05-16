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
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.MensajeError = "";
            try
            {
                TempData["UrlSearch"] = Url.Action("Index", "Posts");
                var posts = _postService.GetPosts();
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

                int pageSize = 6;
                var result = (PaginatedList<PostDto>.Create(posts.ToList(), pageNumber ?? 1, pageSize)).Result;

                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.MensajeError = ex.Message;
                _logger.LogError($"Index => {ex.Message}");
                return View();
            }
        }

        public  IActionResult Registrar(long ID)
        {
            PostViewModel model = new PostViewModel();
            var post =  _postService.GetPost(ID, GetUsuarioId(), false, "");
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
        public  IActionResult VerPost(long ID)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
            bool pantalla = false;
            long usuarioId = 0;

            if (User.Identity.IsAuthenticated)
            {
                usuarioId = GetUsuarioId();
                pantalla = true;
            }
            var result = _postService.GetPost(ID, usuarioId, pantalla, GetIp());
            return View(result);
        }

        [HttpPost]
        public  IActionResult EliminarPost(long ID)
        {
            var result =  _postService.EliminarPost(ID, GetUsuarioId());
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
        public  IActionResult Registrar(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UsuarioId = GetUsuarioId();
                var result =  _postService.AgregarPost(model);
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

        public  IActionResult MisPosts(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            TempData["UrlSearch"] = Url.Action("MisPosts", "Posts");
            var posts =  _postService.GetPosts(GetUsuarioId());
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
            return View((PaginatedList<PostDto>.Create(posts.ToList(), pageNumber ?? 1, pageSize)).Result);
        }

        [Authorize(Roles = "SuperAdministrador,Administrador")]
        public  IActionResult VistasUsuario(long Id)
        {
            return View( _postService.GetVistasUsuario(Id, GetUsuarioId()));
        }

        [Authorize(Roles = "SuperAdministrador,Administrador")]
        public  IActionResult VistasAnonimas(long Id)
        {
            return View( _postService.GetVistasAnonima(Id, GetUsuarioId()));
        }
    }
}
