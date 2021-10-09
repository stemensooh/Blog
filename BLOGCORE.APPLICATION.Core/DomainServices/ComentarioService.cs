using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.DomainServices
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepositorio _comentarioRepositorio;

        public ComentarioService(IComentarioRepositorio comentarioRepositorio)
        {
            _comentarioRepositorio = comentarioRepositorio;
        }

        public async Task<List<ComentariosPostDto>> GetComentariosPostId(long Id)
        {
            List<ComentariosPostDto> comentariosPostDtos = new List<ComentariosPostDto>();
            var comentarios = await _comentarioRepositorio.GetComentariosPostId(Id);
            var comentariosPrincipales = comentarios.Where(x => x.ComentarioPadreId == 0);
            var comentariosSecuendarios = comentarios.Where(x => x.ComentarioPadreId != 0);

            foreach (var comentario in comentariosPrincipales)
            {
                var comentarioDto = new ComentariosPostDto
                {
                    Id = comentario.Id,
                    Mensaje = comentario.Mensaje,
                    Fecha = comentario.FechaCreacion,
                    Usuario = comentario.UsuarioNavigation.Username,
                    NombreCompleto = comentario.UsuarioNavigation.Perfil.Nombres + " " + comentario.UsuarioNavigation.Perfil.Apellidos,
                    Comentarios = CargarComentarioSecuendario(comentario.Id, comentariosSecuendarios.ToList())
                };

                comentariosPostDtos.Add(comentarioDto);
            }

            return comentariosPostDtos;
        }

        private List<ComentariosPostDto> CargarComentarioSecuendario(long Id, List<Entities.Comentario> comentariosSecuendarios)
        {
            List<ComentariosPostDto> comentariosPostDtos = new List<ComentariosPostDto>();
            foreach (var comentario in comentariosSecuendarios.Where(x => x.ComentarioPadreId == Id))
            {
                var comentarioDto = new ComentariosPostDto
                {
                    Id = comentario.Id,
                    Mensaje = comentario.Mensaje,
                    Fecha = comentario.FechaCreacion,
                    Usuario = comentario.UsuarioNavigation.Username,
                    NombreCompleto = comentario.UsuarioNavigation.Perfil.Nombres + " " + comentario.UsuarioNavigation.Perfil.Apellidos,
                    Comentarios = CargarComentarioSecuendario(comentario.Id, comentariosSecuendarios.ToList())
                };

                comentariosPostDtos.Add(comentarioDto);
            }

            return comentariosPostDtos;
        }

    }
}
