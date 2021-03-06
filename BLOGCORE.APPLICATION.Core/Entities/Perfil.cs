using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class Perfil
    {
        [Key]
        public long Id { get; set; }


        [MaxLength(100)]
        public string Nombres { get; set; }


        [MaxLength(100)]
        public string Apellidos { get; set; }

        [MaxLength(500)]
        public string Direccion { get; set; }

        [MaxLength(20)]
        public string Ip { get; set; }

        public long UsuarioId { get; set; }


        public Usuario Usuario { get; set; }
        public virtual List<RedesSociales> RedesSociales { get; set; }
    }
}
