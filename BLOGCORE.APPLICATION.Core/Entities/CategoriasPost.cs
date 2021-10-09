using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class CategoriasPost
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("Post")]
        public long PostId { get; set; }

        public virtual Categoria CategoriaNavigation { get; set; }
        public virtual Post PostNavigation { get; set; }
    }
}
