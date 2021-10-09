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
            Comentarios = new HashSet<Comentario>();
        }

        [Key]
        public long Id { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Ip { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public byte Estado { get; set; }

        public Perfil  Perfil { get; set; }
        public virtual ICollection<PostVistas> Vistas { get; set; }
        public virtual ICollection<UsuarioRol> Roles { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
