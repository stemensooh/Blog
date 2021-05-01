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
        public async Task<List<Post>> GetPostsAsync()
        {
            await using MysqlDbContext db = new MysqlDbContext();
            return await db.Posts.Include(x => x.UsuarioNavigation).Where(x => x.Estado == true).ToListAsync();
        }

        public async Task<List<Post>> GetPostsAsync(long UsuarioId)
        {
            await using MysqlDbContext db = new MysqlDbContext();
            return await db.Posts.Include(x => x.UsuarioNavigation).Where(x => x.UsuarioId == UsuarioId && x.Estado == true).ToListAsync();
        }

        public async Task<Post> GetPostAsync(long PostId, long usuarioId, bool Pantalla)
        {
            await using MysqlDbContext db = new MysqlDbContext();
            var post = await db.Posts.Include(x => x.UsuarioNavigation).Include(x => x.Vistas).FirstOrDefaultAsync(x => x.Id == PostId && x.Estado == true);
            if (Pantalla)
            {
                if (post != null)
                {
                    var vistas = await db.Vistas.Where(x => x.PostId == post.Id).ToListAsync();
                    if (vistas != null)
                    {
                        if (vistas.FirstOrDefault(x => x.UsuarioId == usuarioId) == null)
                        {
                            await db.Vistas.AddAsync(new PostVistas() { UsuarioId = usuarioId, PostId = post.Id, FechaVista = DateTime.Now });
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
            return post;
        }
        
        public async Task<Post> GetPostAsync(long PostId, long UsuarioId)
        {
            await using MysqlDbContext db = new MysqlDbContext();
            return await db.Posts.Include(x => x.UsuarioNavigation).FirstOrDefaultAsync(x => x.Id == PostId && x.UsuarioId == UsuarioId && x.Estado == true);
        }

        public async Task<bool> EditarPostAsync(Post post)
        {
            await using MysqlDbContext db = new MysqlDbContext();
            post.FechaModificacion = DateTime.Now;
            db.Posts.Update(post);
            return await db.SaveChangesAsync() > 0 ? true : false;
        }
        
        public async Task<bool> AgregarPostAsync(Post post)
        {
            await using MysqlDbContext db = new MysqlDbContext();
            post.FechaCreacion = DateTime.Now;
            post.Estado = true;
            await db.Posts.AddAsync(post);
            return await db.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<int> EliminarPostAsync(long PostId, long UsuarioId)
        {
            int result = 0;
            await using MysqlDbContext db = new MysqlDbContext();
            Post post;
            post = await db.Posts.FirstOrDefaultAsync(x => x.Id == PostId && x.UsuarioId == UsuarioId && x.Estado == true);
            if (post is null)
            {
                result = 1; // Post no existe
                post = await db.Posts.FirstOrDefaultAsync(x => x.Id == PostId && x.Estado == true);
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
                db.Posts.Update(post);
                await db.SaveChangesAsync();
            }
            return result;
        }
    }
}
