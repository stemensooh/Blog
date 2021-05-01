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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public async Task<Usuario> GetUsuarioAsync(string Email, string Password)
        {
            await using (var db = new MysqlDbContext())
            {
                return db.Usuarios.Include(x => x.Roles).FirstOrDefault(x => x.Email == Email && x.Password == Password && x.Estado == true);
            }
        }

        public async Task<List<Rol>> GetRolesAsync(int[] roles)
        {
            await using (var db = new MysqlDbContext())
            {
                return await db.Roles.Where(x => roles.Contains(x.Id)).ToListAsync();
            }
        }
    }
}
