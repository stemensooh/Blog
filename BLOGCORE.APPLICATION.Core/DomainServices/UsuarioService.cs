using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.APPLICATION.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.DomainServices
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<RespuestaLoginDto> SignInAsync(UsuarioSignInViewModel model)
        {
            RespuestaLoginDto respuestaLogin = new RespuestaLoginDto();
            var response = await _usuarioRepositorio.SignInAsync(model.Email, model.Password, model.Ip);
            if (response is null)
            {
                respuestaLogin.TieneError = true;
                respuestaLogin.MensajeLogin = "Email o Password son incorrectos";
                return respuestaLogin;
            }

            if(response.Roles != null && response.Roles.Any())
            {
                var roles = await _usuarioRepositorio.GetRolesAsync(response.Roles.Select(c => c.RolId).ToArray());

                if (roles.FirstOrDefault(x => x.Nombre == Constants.Constantes.Rol.Usuario.ToString()) != null)
                    respuestaLogin.Rol = Constants.Constantes.Rol.Usuario.ToString();

                if (roles.FirstOrDefault(x => x.Nombre == Constants.Constantes.Rol.Administrador.ToString()) != null)
                    respuestaLogin.Rol = Constants.Constantes.Rol.Administrador.ToString();

                if (roles.FirstOrDefault(x => x.Nombre == Constants.Constantes.Rol.SuperAdministrador.ToString()) != null) 
                    respuestaLogin.Rol = Constants.Constantes.Rol.SuperAdministrador.ToString();

            }

            respuestaLogin.UsuarioId = response.Id;
            respuestaLogin.Username = response.Username;
            respuestaLogin.Email = response.Email;
            return respuestaLogin;
        }

        public async Task<PerfilDto> VerPerfilAsync(string username, bool EsAdministrador = false)
        {
            var result = await _usuarioRepositorio.GetUsuarioAsync(username, null);
            if (result is null) return null;
            if (result.Perfil is null) return null;
            if (result.Roles is null) return null;
            if (!EsAdministrador)
            {
                if (result.Roles.Where(x => x.RolNavigation.Nombre == Constants.Constantes.Rol.SuperAdministrador.ToString() || x.RolNavigation.Nombre == Constants.Constantes.Rol.Administrador.ToString()).Any()) return null;
            }

            return new PerfilDto() { Apellido = result.Perfil?.Apellidos??"", Nombre = result.Perfil?.Nombres ?? "", Username = result.Username ?? "" } ;
        }

        public async Task<Usuario> SignUpAsync(UsuarioSignUpViewModel model)
        {
            var rol = await _usuarioRepositorio.GetRolAsync(Constants.Constantes.Rol.Usuario.ToString());
            Usuario usuario = new Usuario
            {
                Email = model.Email,
                Password = model.Password,
                Username = model.Username,
                FechaCreacion = DateTime.Now,
                Estado = (int)Constants.Constantes.EstadoUsuario.Activo,

                Perfil = new Perfil
                {
                    Nombres = "Por definir...",
                    Apellidos = "Por definir...",
                    Direccion = "Por definir..."
                }
            };

            usuario.Roles.Add(new UsuarioRol() { RolId = rol.Id});

            var result = await _usuarioRepositorio.AddUsuario(usuario);
            return result > 0 ? usuario : null;
        }

        public async Task<List<AccesoUsuarioDto>> GetAccesosUsuarios()
        {
            var result = await _usuarioRepositorio.GetAccesoUsuarios();
            if (result is null) return null;
            return result.Select(c => new AccesoUsuarioDto(c)).ToList();
        }
    }
}
