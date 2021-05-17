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
    public class PostRepositorio : IPostRepositorio
    {
        private readonly SqlDbContext context;

        public PostRepositorio(SqlDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Post>> GetPosts()
        {
            return await context.Posts.Include(x => x.UsuarioNavigation).Where(x => x.Estado == true).ToListAsync();
        }

        public async Task<List<Post>> GetPosts(long UsuarioId)
        {
            return await context.Posts.Include(x => x.UsuarioNavigation).Include(x => x.Vistas).Include(x => x.VistasAnonimas).Where(x => x.UsuarioId == UsuarioId && x.Estado == true).ToListAsync();
        }

        public async Task<Post> GetPost(long PostId, long usuarioId, bool Pantalla, string Ip)
        {
            var post = await context.Posts.Include(x => x.UsuarioNavigation).Include(x => x.Vistas).Include(x => x.VistasAnonimas).FirstOrDefaultAsync(x => x.Id == PostId && x.Estado == true);
            if (post != null)
            {
                if (usuarioId == 0)
                {
                    await context.VistasAnonimas.AddAsync(new PostVistasAnonimas() { PostId = post.Id, FechaVista = DateTime.Now, Ip = Ip });
                    await context.SaveChangesAsync();
                }

                if (Pantalla)
                {
                    if (usuarioId != post.UsuarioId)
                    {
                        await context.Vistas.AddAsync(new PostVistas() { UsuarioId = usuarioId, PostId = post.Id, FechaVista = DateTime.Now, Ip = Ip });
                        await context.SaveChangesAsync();
                    }
                }

                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }

            return post;
        }

        public  Post GetPost(long PostId, long UsuarioId)
        {
            return  context.Posts.Include(x => x.UsuarioNavigation).FirstOrDefault(x => x.Id == PostId && x.UsuarioId == UsuarioId && x.Estado == true);
        }

        public  bool EditarPost(Post post)
        {
            post.FechaModificacion = DateTime.Now;
            context.Posts.Update(post);
            return  context.SaveChanges() > 0 ? true : false;
        }

        public  bool AgregarPost(Post post)
        {
            post.FechaCreacion = DateTime.Now;
            post.Estado = true;
             context.Posts.Add(post);
            return  context.SaveChanges() > 0 ? true : false;
        }

        public  int EliminarPost(long PostId, long UsuarioId)
        {
            int result = 0;
            Post post;
            post =  context.Posts.FirstOrDefault(x => x.Id == PostId && x.UsuarioId == UsuarioId && x.Estado == true);
            if (post is null)
            {
                result = 1; // Post no existe
                post =  context.Posts.FirstOrDefault(x => x.Id == PostId && x.Estado == true);
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
                 context.SaveChanges();
            }
            return result;
        }

        public  List<PostVistas> GetVistas(long PostId)
        {
            return  context.Vistas.Include(x => x.UsuarioNavigation).ThenInclude(x => x.Perfil).Where(x => x.PostId == PostId).ToList();
        }

        public  List<PostVistasAnonimas> GetVistasAnonima(long PostId)
        {
            return  context.VistasAnonimas.Where(x => x.PostId == PostId).ToList();
        }
    }
}
