using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class PerfilDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public string Direccion { get; set; }

        public PerfilDto(string nombre = null, string apellido = null, string username = null, string direccion = null)
        {
            Nombre = nombre;
            Apellido = apellido;
            Username = username;
            Direccion = direccion;
        }

        public override bool Equals(object obj)
        {
            return obj is PerfilDto dto &&
                   Nombre == dto.Nombre &&
                   Apellido == dto.Apellido &&
                   Username == dto.Username &&
                   Direccion == dto.Direccion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Apellido, Username, Direccion);
        }
    }
}
