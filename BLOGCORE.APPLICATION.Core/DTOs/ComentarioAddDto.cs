using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class ComentarioAddDto
    {
        public long PostId { get; set; }
        public long ComentarioPadreId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El comentario es requerido")]
        public string Comentario { get; set; }
        
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El campo email no es un formato válido")]
        public string Email { get; set; }
        public string Ip { get; set; }
        public string Captcha { get; set; }
    }
}
