using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class PostModelDto
    {
        public long ID { get; set; }
        public string Titulo { get; set; }
        public int Categoria { get; set; }
        public string Cuerpo { get; set; }
        public string Imagen { get; set; }
        public long UsuarioId { get; set; }
    }
}
