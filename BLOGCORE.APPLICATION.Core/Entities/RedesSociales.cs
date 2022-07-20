using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class RedesSociales
    {
        public long RedesSocialesId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Icono { get; set; }

        [Column(TypeName = "varchar(5000)")]
        public string Url { get; set; }
        public bool Estado { get; set; }
        public long PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}
