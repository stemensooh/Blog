using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class PostDto
    {
        public PostDto()
        {

        }

        public PostDto(Post post)
        {
            Vistas = post.Vistas != null && post.Vistas.Any() ? post.Vistas.Count() : 0 ;
            ID = post.Id;
            Titulo = post.Titulo;
            Cuerpo = post.Cuerpo;
            Imagen = post.Imagen;
            FechaCreacion = post.FechaCreacion.ToString("dd/MM/yyyy");
            Autor = post.UsuarioNavigation?.Username??"";
            Username = post.UsuarioNavigation?.Username??"";
            VistasPaginaUsuario = post.VistasPaginaUsuario;
            VistasPaginaAnonimo = post.VistasPaginaAnonimo;
        }

        public long ID { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public string Imagen { get; set; }
        public string FechaCreacion { get; set; }
        public string Username { get; set; }
        public string Autor { get; set; }
        public long Vistas { get; set; }
        public long VistasPaginaUsuario { get; set; }
        public long VistasPaginaAnonimo { get; set; }
    }
}
