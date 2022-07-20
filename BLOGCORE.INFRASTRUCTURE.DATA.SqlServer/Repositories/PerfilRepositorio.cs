using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Repositories
{
    public class PerfilRepositorio : IPerfilRepositorio
    {
        private readonly SqlDbContext context;

        public PerfilRepositorio(SqlDbContext context)
        {
            this.context = context;
        }

        public List<TipoRedSocial> GetTipoRedSocial()
        {
            return context.TipoRedSocial.Where(x => x.Estado).ToList();
        }

        public List<RedesSociales> GetMisRedes(string username)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Username == username);
            if (usuario != null)
            {
                var perfil = context.Perfiles.FirstOrDefault(x => x.UsuarioId == usuario.Id);
                if (perfil != null)
                {
                    return context.RedesSociales.Where(x => x.PerfilId == perfil.Id && x.Estado).ToList();
                }
            }
            
            return new List<RedesSociales>();
        }
    }
}
