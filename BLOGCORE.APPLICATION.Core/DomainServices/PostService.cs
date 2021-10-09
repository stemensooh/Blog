using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.APPLICATION.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.DomainServices
{
    public class PostService : IPostService
    {
        private readonly IPostRepositorio _postRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public PostService(IPostRepositorio postRepositorio, ICategoriaRepositorio categoriaRepositorio)
        {
            _postRepositorio = postRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
        }

        public async Task<List<PostDto>> GetPosts()
        {
            List<PostDto> postDtos = new List<PostDto>();
            var posts = await _postRepositorio.GetPosts();
            postDtos = posts.Select(c => new PostDto(c)).ToList();
            return postDtos;
        }

        public async Task<PostDto> GetPost(long PostId, long usuarioId, bool Pantalla, string Ip)
        {
            PostDto postDto = new PostDto();
            var post = await _postRepositorio.GetPost(PostId, usuarioId, Pantalla, Ip);
            if (post is null)
            {
                return null;
            }
            postDto = new PostDto(post);
            return postDto;
        }

        public async Task<List<PostDto>> GetPosts(long UsuarioId)
        {
            List<PostDto> postDtos = new List<PostDto>();
            var posts = await  _postRepositorio.GetPosts(UsuarioId);
            postDtos = posts.Select(c => new PostDto(c)).ToList();
            return postDtos;
        }
        
        public async Task<List<PostDto>> GetPosts(int Top)
        {
            List<PostDto> postDtos = new List<PostDto>();
            var posts = await  _postRepositorio.GetPosts(Top);
            postDtos = posts.Select(c => new PostDto(c)).ToList();
            return postDtos;
        }

        public  bool AgregarPost(PostViewModel model)
        {
            Post post = new Post();
            if (model.Id > 0)
            {
                post =  _postRepositorio.GetPost(model.Id, model.UsuarioId);
                if (post is null)
                {
                    return false;
                }

                post.Titulo = model.Titulo;
                post.Cuerpo = model.Cuerpo;
                post.Imagen = model.Imagen;
                post = _postRepositorio.EditarPost(post);
                if (post is null) return false;

                _categoriaRepositorio.LimpiarDetalleCategoria(post.Id);
                foreach (var categoriaId in model.Categoria)
                {
                    if (categoriaId > 0)
                    {
                        _categoriaRepositorio.GuardarDetalle(post.Id, categoriaId);
                    }
                }

                return true;
            }
            else
            {
                string cuerpo = model.Cuerpo;
                post = new Post
                {
                    Id = model.Id,
                    Titulo = model.Titulo,
                    Cuerpo = cuerpo,
                    Imagen = model.Imagen,
                    UsuarioId = model.UsuarioId
                };

                post = _postRepositorio.AgregarPost(post);
                if (post is null) return false;

                foreach (var categoriaId in model.Categoria)
                {
                    if (categoriaId > 0)
                    {
                        _categoriaRepositorio.GuardarDetalle(post.Id, categoriaId);
                    }
                }

                return true;
            }
        }

        public  int EliminarPost(long PostId, long UsuarioId)
        {
            return  _postRepositorio.EliminarPost(PostId, UsuarioId);
        }

        public  List<UsuarioDto> GetVistasUsuario(long PostId, long UsuarioId)
        {
            var existe =  _postRepositorio.GetPost(PostId, UsuarioId);
            if (existe is null) return null;

            var result =  _postRepositorio.GetVistas(PostId);
            if (result is null) return null;

            var usuarios = new List<UsuarioDto>();
            foreach (var item in result)
            {
                var usuario = new UsuarioDto();
                usuario.Apellidos = item.UsuarioNavigation.Perfil.Apellidos;
                usuario.Nombres = item.UsuarioNavigation.Perfil.Nombres;
                usuario.Username = item.UsuarioNavigation.Username;
                usuario.Email = item.UsuarioNavigation.Email;
                usuario.Id = item.Id;
                usuario.FechaVista = item.FechaVista.ToString("dd/MM/yyyy HH:mm:ss");
                usuario.Ip = item.Ip ?? "";
                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public  List<UsuarioDto> GetVistasAnonima(long PostId, long UsuarioId)
        {
            var existe =  _postRepositorio.GetPost(PostId, UsuarioId);
            if (existe is null) return null;

            var result =  _postRepositorio.GetVistasAnonima(PostId);
            if (result is null) return null;

            var usuarios = new List<UsuarioDto>();
            foreach (var item in result)
            {
                var usuario = new UsuarioDto();
                usuario.Username = "Anonimo";
                usuario.Id = item.Id;
                usuario.FechaVista = item.FechaVista.ToString("dd/MM/yyyy HH:mm:ss");
                usuario.Ip = item.Ip ?? "";
                usuarios.Add(usuario);
            }

            return usuarios;
        }
    }
}
