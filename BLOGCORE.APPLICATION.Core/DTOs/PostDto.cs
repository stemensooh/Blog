using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
            ID = post.Id;
            Titulo = post.Titulo;
            Categoria = post.Categoria?.Descripcion ?? "";
            CategoriaId = post.CategoriaId;
            Cuerpo = post.Cuerpo;
            Imagen = post.Imagen;
            Fecha = post.FechaCreacion;
            FechaCreacion = post.FechaCreacion.ToString("dd/MM/yyyy");
            Autor = post.Usuario?.Username ?? "";
            Username = post.Usuario?.Username ?? "";
            VistasPaginaAnonimo = post.VistasAnonimas != null && post.VistasAnonimas.Any() ? post.VistasAnonimas.Count() : 0;
            VistasPaginaUsuario = post.Vistas != null && post.Vistas.Any() ? post.Vistas.Count() : 0;
            Vistas = post.Vistas != null && post.Vistas.Any() ? post.Vistas.GroupBy(x => x.UsuarioId).Count() : 0;
        }

        public long ID { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public int CategoriaId { get; set; }
        public string Cuerpo { get; set; }
        public string Imagen { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaCreacion { get; set; }
        public string Username { get; set; }
        public string Autor { get; set; }
        public long Vistas { get; set; }
        public long VistasPaginaUsuario { get; set; }
        public long VistasPaginaAnonimo { get; set; }
    }
}
