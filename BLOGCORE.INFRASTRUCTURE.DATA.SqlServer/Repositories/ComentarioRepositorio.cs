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
    public class ComentarioRepositorio : IComentarioRepositorio
    {
        private readonly SqlDbContext context;

        public ComentarioRepositorio(SqlDbContext context)
        {
            this.context = context;
        }

        public Task<int> Add(Comentario comentario)
        {
            var post = context.Posts.Find(comentario.PostId);
            
            if (post != null)
            {
                post.TotalComentarios += 1;
                if (comentario.ComentarioPadreId > 0)
                {
                    if (context.Comentarios.Find(comentario.ComentarioPadreId) != null)
                    {
                        context.Comentarios.AddAsync(comentario);
                    }
                }
                else
                {
                    context.Comentarios.AddAsync(comentario);
                }
            }

            return context.SaveChangesAsync();
        }

        public async Task<List<Comentario>> GetComentariosPostId(long Id) 
        {
            return await context.Comentarios
                //.Include(x => x.UsuarioNavigation)
                //.ThenInclude(x => x.Perfil)
                .Where(x => x.PostId == Id && x.Estado)
                .ToListAsync();
        }
    }
}
