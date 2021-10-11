using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class AccesoUsuario
    {
        [Key]
        public long Id { get; set; }
        public long UsuarioId { get; set; }

        [MaxLength(20)]
        public string Ip { get; set; }

        public int TipoAcceso { get; set; }

        [MaxLength(500)]
        public string DescripcionAcceso { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public DateTime FechaAcceso { get; set; }

        //public virtual Usuario Usuario { get; set; }
    }

}
