using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class Comentario
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string Mensaje { get; set; }

        public long ComentarioPadreId { get; set; }
        public long TotalReaccion { get; set; }



        [ForeignKey("Usuario")]
        public long? UsuarioId { get; set; }

        [ForeignKey("Post")]
        public long PostId { get; set; }

        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string NombreCompleto { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Ip { get; set; }

        public virtual Usuario UsuarioNavigation { get; set; }
        public virtual Post PostNavigation { get; set; }

    }
}
