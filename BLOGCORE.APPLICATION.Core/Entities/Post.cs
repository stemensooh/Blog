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
            VistasAnonimas = new HashSet<PostVistasAnonimas>();
            Categorias = new HashSet<CategoriasPost>();
            Comentarios = new HashSet<Comentario>();
        }

        [Key]
        public long Id { get; set; }

        //[Column(TypeName = "varchar(200)")]
        [MaxLength(200)]
        public string Titulo { get; set; }

        [MaxLength(50000)]
        //[Column(TypeName = "varchar(8000)")]
        public string Cuerpo { get; set; }

        [MaxLength(20)]
        public string Ip { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }


        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }

        public bool Estado { get; set; }

        [Column(TypeName = "varchar(5000)")]
        public string Imagen { get; set; }

        //public long VistasPaginaUsuario { get; set; }
        //public long VistasPaginaAnonimo { get; set; }

        public virtual Usuario Usuario { get; set; }
        //public virtual Categoria Categoria { get; set; }
        //public virtual int CategoriaId { get; set; }
        public long TotalVistas { get; set; }
        public long TotalVistasAnonimas { get; set; }
        public long TotalComentarios { get; set; }

        public virtual ICollection<CategoriasPost> Categorias { get; set; }
        public virtual ICollection<PostVistas> Vistas { get; set; }
        public virtual ICollection<PostVistasAnonimas> VistasAnonimas { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

    }
}
