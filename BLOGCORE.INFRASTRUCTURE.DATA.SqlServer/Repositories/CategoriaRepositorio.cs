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
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly SqlDbContext context;

        public CategoriaRepositorio(SqlDbContext context)
        {
            this.context = context;
        }


        public async Task<List<Categoria>> GetAll()
        {
            return await context.Categorias.ToListAsync();
        }

        public void GuardarDetalle(long Post, int Categoria)
        {
            context.CategoriasPosts.Add(new CategoriasPost { CategoriaId = Categoria, PostId = Post });
            context.SaveChanges();
        }

        public void LimpiarDetalleCategoria(long Post)
        {
            var result = context.CategoriasPosts.Where(x => x.PostId == Post);
            context.RemoveRange(result);
            context.SaveChanges();
        }
    }
}
