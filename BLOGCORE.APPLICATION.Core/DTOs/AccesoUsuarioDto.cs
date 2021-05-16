using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class AccesoUsuarioDto
    {
        public AccesoUsuarioDto()
        {

        }

        public AccesoUsuarioDto(AccesoUsuario accesoUsuario)
        {
            Id = accesoUsuario.Id;
            UsuarioId = accesoUsuario.UsuarioId;
            Ip = accesoUsuario.Ip;
            TipoAcceso = accesoUsuario.TipoAcceso;
            DescripcionAcceso = accesoUsuario.DescripcionAcceso;
            Password = accesoUsuario.Password;
            Email = accesoUsuario.Email;
            FechaAcceso = accesoUsuario.FechaAcceso.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public string Ip { get; set; }
        public int TipoAcceso { get; set; }
        public string DescripcionAcceso { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FechaAcceso { get; set; }
    }
}
