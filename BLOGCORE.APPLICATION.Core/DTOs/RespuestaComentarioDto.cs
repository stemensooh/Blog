using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class RespuestaComentarioDto
    {
        public long TotalComentario { get; set; }
        public List<ComentariosPostDto> Comentarios { get; set; }
    }
}
