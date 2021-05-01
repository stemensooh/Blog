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

        public PostService(IPostRepositorio postRepositorio)
        {
            _postRepositorio = postRepositorio;
        }

        public async Task<List<PostDto>> GetPostsAsync()
        {
            List<PostDto> postDtos = new List<PostDto>();
            var posts = await _postRepositorio.GetPostsAsync();
            postDtos = posts.Select(c => new PostDto(c)).ToList();
            return postDtos;
        }

        public async Task<PostDto> GetPostAsync(long PostId, long usuarioId, bool Pantalla)
        {
            PostDto postDto = new PostDto();
            var post = await _postRepositorio.GetPostAsync(PostId, usuarioId, Pantalla);
            if (post is null)
            {
                return null;
            }
            postDto = new PostDto(post);
            return postDto;
        }

        public async Task<List<PostDto>> GetPostsAsync(long UsuarioId)
        {
            List<PostDto> postDtos = new List<PostDto>();
            var posts = await _postRepositorio.GetPostsAsync(UsuarioId);
            postDtos = posts.Select(c => new PostDto(c)).ToList();
            return postDtos;
        }

        public async Task<bool> AgregarPostAsync(PostViewModel model)
        {
            Post post = new Post();
            if (model.ID > 0)
            {
                post = await _postRepositorio.GetPostAsync(model.ID, model.UsuarioId);
                if (post is null)
                {
                    return false;
                }
                post.Titulo = model.Titulo;
                post.Cuerpo = model.Cuerpo;
                post.Imagen = model.Imagen;
                return await _postRepositorio.EditarPostAsync(post);
            }
            else
            {
                post.Id = model.ID;
                post.Titulo = model.Titulo;
                post.Cuerpo = model.Cuerpo;
                post.Imagen = model.Imagen;
                post.UsuarioId = model.UsuarioId;
                
                return await _postRepositorio.AgregarPostAsync(post);
            }
        }

        public async Task<int> EliminarPostAsync(long PostId, long UsuarioId)
        {
            return await _postRepositorio.EliminarPostAsync(PostId, UsuarioId);
        }
    }
}
