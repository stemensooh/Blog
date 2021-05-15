using BLOGCORE.APPLICATION.Core.Entities;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.INFRASTRUCTURE.DATA.Mysql.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.INFRASTRUCTURE.DATA.Mysql.Repositories
{
    public class PostRepositorio : IPostRepositorio
    {
        private readonly MysqlDbContext context;

        public PostRepositorio(MysqlDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            await using MysqlDbContext db = new MysqlDbContext();
            return await context.Posts.Include(x => x.UsuarioNavigation).Where(x => x.Estado == true).ToListAsync();
        }

        public async Task<List<Post>> GetPostsAsync(long UsuarioId)
        {
            return await context.Posts.Include(x => x.UsuarioNavigation).Include(x => x.Vistas).Where(x => x.UsuarioId == UsuarioId && x.Estado == true).ToListAsync();
        }

        public async Task<Post> GetPostAsync(long PostId, long usuarioId, bool Pantalla)
        {
            var post = await context.Posts.Include(x => x.UsuarioNavigation).Include(x => x.Vistas).FirstOrDefaultAsync(x => x.Id == PostId && x.Estado == true);
            if (post != null)
            {
                post.VistasPaginaAnonimo += 1;
                if (Pantalla)
                {
                    if (usuarioId != post.UsuarioId)
                    {
                        post.VistasPaginaUsuario += 1;
                    }

                    var vistas = await context.Vistas.Where(x => x.PostId == post.Id).ToListAsync();
                    if (vistas != null)
                    {
                        if (vistas.FirstOrDefault(x => x.UsuarioId == usuarioId) == null)
                        {
                            await context.Vistas.AddAsync(new PostVistas() { UsuarioId = usuarioId, PostId = post.Id, FechaVista = DateTime.Now });
                            await context.SaveChangesAsync();
                        }
                    }
                }

                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }

            return post;
        }
        
        public async Task<Post> GetPostAsync(long PostId, long UsuarioId)
        {
            return await context.Posts.Include(x => x.UsuarioNavigation).FirstOrDefaultAsync(x => x.Id == PostId && x.UsuarioId == UsuarioId && x.Estado == true);
        }

        public async Task<bool> EditarPostAsync(Post post)
        {
            post.FechaModificacion = DateTime.Now;
            context.Posts.Update(post);
            return await context.SaveChangesAsync() > 0 ? true : false;
        }
        
        public async Task<bool> AgregarPostAsync(Post post)
        {
            post.FechaCreacion = DateTime.Now;
            post.Estado = true;
            await context.Posts.AddAsync(post);
            return await context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<int> EliminarPostAsync(long PostId, long UsuarioId)
        {
            int result = 0;
            Post post;
            post = await context.Posts.FirstOrDefaultAsync(x => x.Id == PostId && x.UsuarioId == UsuarioId && x.Estado == true);
            if (post is null)
            {
                result = 1; // Post no existe
                post = await context.Posts.FirstOrDefaultAsync(x => x.Id == PostId && x.Estado == true);
                if (post is null)
                {
                    result = 2; // Post de otro usuario
                }
            }
            else
            {
                result = 3; //Eliminado
                post.FechaEliminacion = DateTime.Now;
                post.Estado = false;
                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<PostVistas>> GetVistasAsync(long PostId)
        {
            return await context.Vistas.Include(x => x.UsuarioNavigation).ThenInclude(x => x.Perfil).Where(x => x.PostId == PostId).ToListAsync();
        }
    }
}
