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

        public async Task<Usuario> SignInAsync(string Email, string Password, string Ip)
        {
            AccesoUsuario acceso = new AccesoUsuario();
            acceso.Ip = Ip;
            acceso.Email = Email;
            acceso.Password = Password;
            acceso.Ip = Ip;
            acceso.FechaAcceso = DateTime.Now;
            acceso.TipoAcceso = (byte)APPLICATION.Core.Constants.Constantes.AccesoUsuario.exitoso;
            acceso.DescripcionAcceso = "Acceso exitoso.";

            Usuario usr = await context.Usuarios.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password && x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.Activo);
            if (usr is null)
            {
                acceso.TipoAcceso = (byte)APPLICATION.Core.Constants.Constantes.AccesoUsuario.fallido;
                Usuario existe = await context.Usuarios.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == Email && x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.Activo);
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

            await context.AccesoUsuarios.AddAsync(acceso);
            await context.SaveChangesAsync();

            return usr;
        }

        public async Task<List<Rol>> GetRolesAsync(int[] roles)
        {
            return await context.Roles.Where(x => roles.Contains(x.Id)).ToListAsync();
        }

        public async Task<Usuario> GetUsuarioAsync(string username, string email)
        {
            return await context.Usuarios.Include(x => x.Roles).ThenInclude(x => x.RolNavigation).Include(x => x.Perfil).FirstOrDefaultAsync(x => (x.Email == email || x.Username == username) && (x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.Activo || x.Estado == (int)APPLICATION.Core.Constants.Constantes.EstadoUsuario.PorConfirmar));
        }

        public async Task<int> AddUsuario(Usuario usuario)
        {
            await context.Usuarios.AddAsync(usuario);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddRol(Rol rol)
        {
            await context.Roles.AddAsync(rol);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddPerfil(Perfil perfil)
        {
            await context.Perfiles.AddAsync(perfil);
            return await context.SaveChangesAsync();
        }

        public async Task<Rol> GetRolAsync(string rol)
        {
            return await context.Roles.FirstOrDefaultAsync(x => x.Nombre == rol);
        }

        public async Task<List<AccesoUsuario>> GetAccesoUsuarios()
        {
            return await context.AccesoUsuarios.ToListAsync();
        }
    }
}
