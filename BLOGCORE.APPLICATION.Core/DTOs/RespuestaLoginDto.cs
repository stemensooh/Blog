using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class RespuestaLoginDto
    {
        public long UsuarioId { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public bool TieneError { get; set; }
        public string MensajeLogin { get; set; }
    }
}
