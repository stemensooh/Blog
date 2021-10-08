using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Storage;
using BLOGCORE.APPLICATION.Core.ViewModels;
using BLOGCORE.UI.Website.Angular.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Angular.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BaseController
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IStorageService _storageService;
        private string mensaje;

        public PostsController(ILogger<PostsController> logger, IPostService postService, IWebHostEnvironment hostEnvironment, IConfiguration configuration, IStorageService storageService)
        {
            _logger = logger;
            _postService = postService;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
            _storageService = storageService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize = 5)
        {
            //ViewBag.MensajeError = "";
            try
            {
                List<PostDto> posts = new List<PostDto>();
                //TempData["UrlSearch"] = Url.Action("Index", "Posts");
                var resultPosts = await _postService.GetPosts();

                foreach (var item in resultPosts)
                {
                    posts.Add(MapPost(item));
                }

                //ViewBag.CantidadPosts = posts != null && posts.Any() ? posts.Count() : 0;

                //ViewData["CurrentSort"] = sortOrder;
                //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                //ViewData["CurrentFilter"] = searchString;

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
                    case "date_asc":
                        posts = posts.OrderBy(s => s.FechaCreacion).ToList();
                        break;
                    case "date_desc":
                        posts = posts.OrderByDescending(s => s.FechaCreacion).ToList();
                        break;
                    default:
                        posts = posts.OrderBy(s => s.Titulo).ToList();
                        break;
                }

                var result = await (PaginatedList<PostDto>.Create(posts.ToList(), pageNumber ?? 1, pageSize));

                return Ok(result);
            }
            catch (Exception ex)
            {
                //ViewBag.MensajeError = ex.Message;
                _logger.LogError($"Index => {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPost("Registrar")]
        public IActionResult Registrar(PostSiteDto model)
        {
            if (ModelState.IsValid)
            {
                MemoryStream ms = null;
                byte[] bytes = System.Convert.FromBase64String(model.ImagenBase64);
                if (bytes != null && bytes.Length > 0)
                {
                    ms = new MemoryStream(bytes);
                }
                string RutaImagen = "";
                

                if (model.Imagen == null)
                {
                    RutaImagen = model.ImagenRuta;
                }
                else
                {
                    if (_configuration["TipoAlmacenamiento"] == "1")
                    {
                        RutaImagen = Path.Combine(_configuration["DirectorioImagenes"], Guid.NewGuid().ToString() + "-" + model.Imagen );
                        if (ms != null)
                        {
                            _storageService.GuardarArchivo(ms, Path.Combine("wwwroot", RutaImagen), ref mensaje);
                        }
                    }
                    else
                    {
                        if (ms != null)
                        {
                            RutaImagen = Path.Combine(_configuration["DirectorioImagenes"], Guid.NewGuid().ToString() + "-" + model.Imagen);
                            _storageService.GuardarArchivo(ms, RutaImagen, ref mensaje, "images");
                        }
                    }
                }

                PostViewModel post = new PostViewModel
                {
                    ID = model.ID,
                    Imagen = RutaImagen,
                    Titulo = model.Titulo,
                    Categoria = model.Categoria,
                    Cuerpo = model.Cuerpo,
                    UsuarioId = GetUsuarioId()
                };

                var result = _postService.AgregarPost(post);
                if (result)
                {
                    return Created("", post);
                }
                else
                {
                    BadRequest("No se ha podido actualizar el registro correctamente");
                }
            }
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("VerPost/{ID}")]
        public async Task<IActionResult> VerPost(long ID)
            {
            try
            {
                bool pantalla = false;
                long usuarioId = 0;

                if (User.Identity.IsAuthenticated)
                {
                    usuarioId = GetUsuarioId();
                    pantalla = true;
                }

                var result = await _postService.GetPost(ID, usuarioId, pantalla, GetIp());
                if (result != null)
                {
                    result.Imagen = ObtenerImagenBase64(_configuration["TipoAlmacenamiento"], result.Imagen, "");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("EliminarPost/{ID}")]
        public  IActionResult EliminarPost(long ID)
        {
            var result =  _postService.EliminarPost(ID, GetUsuarioId());
            return result switch
            {
                1 => Ok(new ResultViewDto() { Estado = false, Mensaje = "Registro inexistente" }),
                2 => Forbid(),
                3 => Ok(new ResultViewDto() { Estado = true, Mensaje = "Registro eliminado" }),
                _ => Ok(new ResultViewDto() { Estado = false, Mensaje = "Registro inexistente" }),
            };
        }

        [Route("MisPosts")]
        public async Task<IActionResult> MisPosts(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize = 5)
        {
            //TempData["UrlSearch"] = Url.Action("MisPosts", "Posts");

            List<PostDto> posts = new List<PostDto>();
            var resultPosts = await _postService.GetPosts(GetUsuarioId());

            foreach (var item in resultPosts)
            {
                posts.Add(MapPost(item));
            }

            //ViewBag.CantidadPosts = posts != null && posts.Any() ? posts.Count() : 0;

            //ViewData["CurrentSort"] = sortOrder;
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //ViewData["CurrentFilter"] = searchString;

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

            return Ok((PaginatedList<PostDto>.Create(posts.ToList(), pageNumber ?? 1, pageSize)).Result);
        }

        [Authorize(Roles = "SuperAdministrador,Administrador")]
        [HttpGet("VistasUsuario/{Id}")]
        public  IActionResult VistasUsuario(long Id)
        {
            return Ok( _postService.GetVistasUsuario(Id, GetUsuarioId()));
        }

        [Authorize(Roles = "SuperAdministrador,Administrador")]
        [HttpGet("VistasAnonimas/{Id}")]
        public IActionResult VistasAnonimas(long Id)
        {
            return Ok( _postService.GetVistasAnonima(Id, GetUsuarioId()));
        }

        [AllowAnonymous]
        [HttpGet("VerPostsRecientes")]
        public async Task<IActionResult> VerPostsRecientes()
        {
            List<PostDto> posts = new List<PostDto>();
            var resultPosts = await _postService.GetPosts(3);
            foreach (var item in resultPosts)
            {
                posts.Add(MapPost(item));
            }

            return Ok(posts);
        }

        #region Metodos privados

        private PostDto MapPost(PostDto item)
        {
            var post = new PostDto();
            post.ID = item.ID;
            post.Titulo = item.Titulo;
            post.Cuerpo = item.Cuerpo;
            post.Imagen = ObtenerImagenBase64(_configuration["TipoAlmacenamiento"], item.Imagen, "");
            post.Fecha = item.Fecha;
            post.FechaCreacion = item.FechaCreacion;
            post.Autor = item.Autor;
            post.Username = item.Username;
            post.VistasPaginaAnonimo = item.VistasPaginaAnonimo;
            post.VistasPaginaUsuario = item.VistasPaginaUsuario;
            post.Vistas = item.Vistas;
            return post;
        }

        private string UploadedFile(IFormFile Imagen)
        {
            string uniqueFileName = null;

            if (Imagen != null)
            {
                if (!System.IO.Directory.Exists(StartupDto.UploadsFolder))
                {
                    System.IO.Directory.CreateDirectory(StartupDto.UploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Imagen.FileName;
                string filePath = Path.Combine(StartupDto.UploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Imagen.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private string ObtenerImagenBase64(string TipoAlmacenamiento, string Imagen, string FileName)
        {
            MemoryStream ms = null;
            if (TipoAlmacenamiento == "1")
            {
                ms = _storageService.ObtenerArchivo(Path.Combine("wwwroot", Imagen), ref mensaje, "images");
            }
            else
            {
                ms = _storageService.ObtenerArchivo(Imagen, ref mensaje, "images");
            }

            return ms == null ? "" : Convert.ToBase64String(ms.ToArray());
        
        }

        #endregion

        //{
        //    string RutaImagen;
        //    MemoryStream ms = null;
        //    if (TipoAlmacenamiento == "1")
        //    {
        //        RutaImagen = Path.Combine(_configuration["DirectorioImagenes"], Guid.NewGuid().ToString() + "_" + FileName);
        //        if (ms != null)
        //        {
        //            _iIOService.GuardarArchivo(ms, Path.Combine("wwwroot", RutaImagen), ref mensaje);
        //        }
        //    }
        //    else
        //    {
        //        if (ms != null)
        //        {
        //            RutaImagen = Guid.NewGuid().ToString() + "_" + FileName;
        //            _iIOService.GuardarArchivo(ms, RutaImagen, ref mensaje, "images");
        //        }
        //    }
        //}
    }
}
