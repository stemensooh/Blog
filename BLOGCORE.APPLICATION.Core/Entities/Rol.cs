using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<UsuarioRol>();
        }
        [Key]
        public int Id { get; set; }

        //[Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        //[Column(TypeName = "varchar(200)")]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        //[NotMapped]
        public virtual ICollection<UsuarioRol> Usuarios { get; set; }
    }
}
