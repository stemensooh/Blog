using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.APPLICATION.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.INFRASTRUCTURE.DATA.Mysql.Data
{
    public class MysqlDbContext : DbContext
    {
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<AccesoUsuario> AccesoUsuarios { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuariosRol { get; set; }
        public DbSet<PostVistas> Vistas { get; set; }
        public DbSet<PostVistasAnonimas> VistasAnonimas { get; set; }

        public string DefaultConecctionString = string.Empty;

        public MysqlDbContext(DbContextOptions<MysqlDbContext> options)
            : base(options)
        {
            Input();
        }

        public MysqlDbContext(string ConnectionString)
        {
            this.DefaultConecctionString = ConnectionString;
        }


        public void Input()
        {
            //appConfig = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText("appsettings.json"));
            this.DefaultConecctionString = StartupDto.ConnectionStringMysql;
        }

        public MysqlDbContext()
        {
            Input();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (this.DefaultConecctionString == null)
                {
                    Input();
                }
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=edocdevreg5.cynm49sjbwty.us-east-1.rds.amazonaws.com;Initial Catalog=EPColombia;User ID=usredocpmcolombia;Password=usredocpmcolombia2020;Connection Timeout=180;");
                optionsBuilder.UseMySQL(this.DefaultConecctionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
           .HasOne(a => a.Perfil)
           .WithOne(b => b.Usuario)
           .HasForeignKey<Perfil>(b => b.UsuarioId);
        }

    }
}
