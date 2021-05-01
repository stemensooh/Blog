using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            Vistas = new HashSet<PostVistas>();
            Roles = new HashSet<UsuarioRol>();
        }
        [Key]
        public long Id { get; set; }

        [MaxLength(50)]
        //[Column(TypeName = "varchar(50)")]
        public string Username { get; set; }

        [MaxLength(100)]
        //[Column(TypeName = "varchar(100)")]
        public string Password { get; set; }

        [MaxLength(100)]
        //[Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [MaxLength(100)]
        //[Column(TypeName = "varchar(100)")]
        public string Nombres { get; set; }

        [MaxLength(100)]
        //[Column(TypeName = "varchar(100)")]
        public string Apellidos { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool Estado { get; set; }
        //[NotMapped]
        public virtual ICollection<PostVistas> Vistas { get; set; }
        public virtual ICollection<UsuarioRol> Roles { get; set; }
    }
}
