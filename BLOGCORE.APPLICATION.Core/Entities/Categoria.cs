﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class Categoria
    {
        public Categoria()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}
