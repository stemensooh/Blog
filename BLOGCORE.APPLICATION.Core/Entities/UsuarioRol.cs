using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class UsuarioRol
    {
        [Key]
        public long IdUsuarioRol { get; set; }

        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }

        [ForeignKey("Rol")]
        public int RolId { get; set; }

        public virtual Usuario UsuarioNavigation { get; set; }
        public virtual Rol RolNavigation { get; set; }
    }
}
