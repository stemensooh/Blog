using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class ComentariosPostDto
    {
        public ComentariosPostDto(Comentario comentario)
        {

        }

        public ComentariosPostDto()
        {
            Comentarios = new List<ComentariosPostDto>();
        }

        public long Id { get; set; }
        public string Usuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public List<ComentariosPostDto> Comentarios { get; set; }
    }
}
