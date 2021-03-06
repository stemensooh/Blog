// <auto-generated />
using System;
using BLOGCORE.INFRASTRUCTURE.DATA.Mysql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLOGCORE.INFRASTRUCTURE.DATA.Mysql.Migrations
{
    [DbContext(typeof(MysqlDbContext))]
    partial class MysqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.AccesoUsuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("DescripcionAcceso")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("FechaAcceso")
                        .HasColumnType("datetime");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("TipoAcceso")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("AccesoUsuarios");
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.Perfil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Apellidos")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Direccion")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Nombres")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Perfiles");
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Cuerpo")
                        .HasColumnType("text")
                        .HasMaxLength(50000);

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime");

                    b.Property<string>("Imagen")
                        .HasColumnType("varchar(5000)");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Titulo")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<long>("VistasPaginaAnonimo")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.PostVistas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FechaVista")
                        .HasColumnType("datetime");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Vistas");
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.PostVistasAnonimas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FechaVista")
                        .HasColumnType("datetime");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("VistasAnonimas");
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<byte>("Estado")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Username")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.UsuarioRol", b =>
                {
                    b.Property<long>("IdUsuarioRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("IdUsuarioRol");

                    b.HasIndex("RolId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuariosRol");
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.Perfil", b =>
                {
                    b.HasOne("BLOGCORE.APPLICATION.Core.Entities.Usuario", "Usuario")
                        .WithOne("Perfil")
                        .HasForeignKey("BLOGCORE.APPLICATION.Core.Entities.Perfil", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.Post", b =>
                {
                    b.HasOne("BLOGCORE.APPLICATION.Core.Entities.Usuario", "UsuarioNavigation")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.PostVistas", b =>
                {
                    b.HasOne("BLOGCORE.APPLICATION.Core.Entities.Post", "PostNavigation")
                        .WithMany("Vistas")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BLOGCORE.APPLICATION.Core.Entities.Usuario", "UsuarioNavigation")
                        .WithMany("Vistas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.PostVistasAnonimas", b =>
                {
                    b.HasOne("BLOGCORE.APPLICATION.Core.Entities.Post", "PostNavigation")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BLOGCORE.APPLICATION.Core.Entities.UsuarioRol", b =>
                {
                    b.HasOne("BLOGCORE.APPLICATION.Core.Entities.Rol", "RolNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BLOGCORE.APPLICATION.Core.Entities.Usuario", "UsuarioNavigation")
                        .WithMany("Roles")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
