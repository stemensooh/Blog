using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.ViewModels
{
    public class PostViewModel
    {
        public long ID { get; set; }

        public string Titulo { get; set; }

        public string Cuerpo { get; set; }

        public string Imagen { get; set; }
        public long UsuarioId { get; set; }
    }
}
