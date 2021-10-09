using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class CategoriaDto
    {
        public CategoriaDto(Categoria categoria)
        {
            Id = categoria.Id;
            Name = categoria.Descripcion;
        }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}
