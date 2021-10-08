using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SqlDbContext context;

        public UsuarioRepositorio(SqlDbContext context)
        {
            this.context = context;
        }

        public Usuario SignIn(string Email, string Password, string Ip)
        {
            AccesoUsuario acceso = new AccesoUsuario();
            acceso.Ip = Ip;
            acceso.Email = Email;
            acceso.Password = Password;
            acceso.Ip = Ip;
            acceso.FechaAcceso = DateTime.Now;
            acceso.TipoAcceso = (byte)APPLICATION.Core.Constants.Constantes.AccesoUsuario.exitoso;
            acceso.DescripcionAcceso = "Acceso exitoso.";

            Usuario usr = context.Usuarios.Include(x => x.Perfil).Include(x => x.Roles).FirstOrDefault(x => x.Email == Email && x.Password == Password && x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.Activo);
            if (usr is null)
            {
                acceso.TipoAcceso = (byte)APPLICATION.Core.Constants.Constantes.AccesoUsuario.fallido;
                Usuario existe = context.Usuarios.Include(x => x.Roles).FirstOrDefault(x => x.Email == Email && x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.Activo);
                if (existe != null)
                {
                    acceso.UsuarioId = existe.Id;
                    acceso.DescripcionAcceso = "Acceso fallido. Password es incorrecto.";
                }
                else
                {
                    acceso.DescripcionAcceso = "Acceso fallido. Credenciales desconocidas.";
                }
            }
            else
            {
                acceso.UsuarioId = usr.Id;
            }

            if (string.IsNullOrEmpty(acceso.Ip))
            {
                acceso.DescripcionAcceso += " La Ip ingresada no es valida";
            }

            context.AccesoUsuarios.Add(acceso);
            context.SaveChanges();

            return usr;
        }

        public async Task<Usuario> CurrentUser(string username)
        {
            return await context.Usuarios
                .Include(x => x.Perfil)
                .Include(x => x.Roles)
                .ThenInclude(x => x.RolNavigation)
                .FirstOrDefaultAsync(x => x.Username == username && x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.Activo);
        }

        public List<Rol> GetRoles(int[] roles)
        {
            return context.Roles.Where(x => roles.Contains(x.Id)).ToList();
        }

        public Usuario GetUsuario(string username, string email)
        {
            return context.Usuarios.Include(x => x.Roles).ThenInclude(x => x.RolNavigation).Include(x => x.Perfil).FirstOrDefault(x => (x.Email == email || x.Username == username) && (x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.Activo || x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.PorConfirmar));
        }

        public int AddUsuario(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            return context.SaveChanges();
        }

        public int AddRol(Rol rol)
        {
            context.Roles.Add(rol);
            return context.SaveChanges();
        }

        public int AddPerfil(Perfil perfil)
        {
            context.Perfiles.Add(perfil);
            return context.SaveChanges();
        }

        public Rol GetRol(string rol)
        {
            return context.Roles.FirstOrDefault(x => x.Nombre == rol);
        }

        public List<AccesoUsuario> GetAccesoUsuarios()
        {
            return context.AccesoUsuarios.ToList();
        }
    }
}
