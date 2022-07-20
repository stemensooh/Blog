using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class TipoRedSocial
    {
        public int TipoRedSocialId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Icono { get; set; }
        public bool Estado { get; set; }
    }
}
