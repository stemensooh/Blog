using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class PostVistas
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }

        [ForeignKey("Post")]
        public long PostId { get; set; }

        public DateTime FechaVista { get; set; }

        public virtual Usuario UsuarioNavigation { get; set; }
        public virtual Post PostNavigation { get; set; }
    }
}
