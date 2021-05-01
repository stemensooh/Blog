using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class Post
    {
        public Post()
        {
            Vistas = new HashSet<PostVistas>();
        }

        [Key]
        public long Id { get; set; }

        //[Column(TypeName = "varchar(200)")]
        [MaxLength(200)]
        public string Titulo { get; set; }

        [MaxLength(50000)]
        //[Column(TypeName = "varchar(8000)")]
        public string Cuerpo { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }


        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }

        public bool Estado { get; set; }

        [Column(TypeName = "varchar(5000)")]
        public string Imagen { get; set; }

        //public long? Vistas { get; set; }

        public virtual Usuario UsuarioNavigation { get; set; }

        public virtual ICollection<PostVistas> Vistas { get; set; }
    }
}
