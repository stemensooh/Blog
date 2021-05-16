using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Constants
{
    public class Constantes
    {
        public const string semillaEncriptacion = "S3m1LL4";
        public enum Rol
        {
            Administrador,
            Usuario,
            SuperAdministrador
        }

        public enum EstadoUsuario
        {
            Inactivo,
            Activo,
            PorConfirmar
        }

        public enum AccesoUsuario
        {
            fallido,
            exitoso
        }
    }
}
