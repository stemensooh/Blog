
using BLOGCORE.APPLICATION.Core.Constants;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.APPLICATION.Core.Utilities;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Inicializador
{
    public class InicializadorDB : IInicializadorDB
    {
        private readonly SqlDbContext _db;

        public InicializadorDB(SqlDbContext db)
        {
            _db = db;
        }

        public void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {
                
            }

            if (_db.Roles.Any(ro => ro.Nombre == Constantes.Rol.SuperAdministrador.ToString())) return;

            var rolAdmin = new APPLICATION.Core.Entities.Rol() { Nombre = Constantes.Rol.SuperAdministrador.ToString(), Descripcion = "" };
            _db.Roles.Add(rolAdmin);
            _db.SaveChanges();
            _db.Roles.Add(new APPLICATION.Core.Entities.Rol() { Nombre = Constantes.Rol.Administrador.ToString(), Descripcion = "" });
            _db.SaveChanges();
            _db.Roles.Add(new APPLICATION.Core.Entities.Rol() { Nombre = Constantes.Rol.Usuario.ToString(), Descripcion = "" });
            _db.SaveChanges();

            var usr = new APPLICATION.Core.Entities.Usuario()
            {
                Email = "stemensooh@gmail.com",
                Estado = (int)Constantes.EstadoUsuario.Activo,
                FechaCreacion = DateTime.Now,
                Password = Crypto.CifrarClave("12345678"),
                Username = "stemensooh",

                Perfil = new APPLICATION.Core.Entities.Perfil()
                {
                    Apellidos = "Administrador",
                    Nombres = "Administrador",
                    Direccion = "",
                }
            };

            _db.Usuarios.Add(usr);
            _db.SaveChanges();

            _db.UsuariosRol.Add(new APPLICATION.Core.Entities.UsuarioRol() { RolId = rolAdmin.Id, UsuarioId = usr.Id });
            _db.SaveChanges();
        }
    }
}
