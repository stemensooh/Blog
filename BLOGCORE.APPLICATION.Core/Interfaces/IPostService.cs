﻿using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDto>> GetPosts();
        List<PostDto> GetPosts(long UsuarioId);
        PostDto GetPost(long PostId, long usuarioId, bool Pantalla, string Ip);
        bool AgregarPost(PostViewModel model);
        int EliminarPost(long PostId, long UsuarioId);
        List<UsuarioDto> GetVistasUsuario(long PostId, long UsuarioId);
        List<UsuarioDto> GetVistasAnonima(long PostId, long UsuarioId);
    }
}
