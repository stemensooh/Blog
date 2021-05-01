using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.ViewModels
{
    public class PostViewModel
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "El titulo es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El Cuerpo es requerido")]
        public string Cuerpo { get; set; }

        public string Imagen { get; set; }
        public long UsuarioId { get; set; }
    }
}
