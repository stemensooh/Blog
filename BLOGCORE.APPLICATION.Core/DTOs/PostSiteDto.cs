using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class PostSiteDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "El titulo es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La categoria es requerida")]
        public int[] Categoria { get; set; }

        [Required(ErrorMessage = "El Cuerpo es requerido")]
        public string Cuerpo { get; set; }

        public bool MantenerImage { get; set; } = true;

        //[Required(ErrorMessage = "La imagen es requerida")]
        public string Imagen { get; set; }
        public string ImagenRuta { get; set; }
        public string ImagenBase64 { get; set; }
        public string Captcha { get; set; }
    }
}
