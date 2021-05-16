using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class UsuarioDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string FechaVista { get; set; }
        public string Ip { get; set; }
    }
}
