using BLOGCORE.APPLICATION.Core.DomainServices;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.INFRASTRUCTURE.DATA.Mysql.Data;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Data;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Inicializador;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            APPLICATION.Core.Utilities.CultureInfoUtil.SetCultureInfo();
            var db = string.IsNullOrEmpty(Configuration["DBActive"]) ? "sql" : Configuration["DBActive"];
            App_Code.AppCode.InitialConfig(Configuration);
            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(double.Parse(Configuration.GetSection("SessionTimeSeconds").Value));
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opciones =>
                {
                    opciones.AccessDeniedPath = "/Pages/AccessDeniedPath";
                    opciones.LoginPath = "/Account/Index";
                    opciones.ExpireTimeSpan = TimeSpan.FromSeconds(double.Parse(Configuration.GetSection("SessionTimeSeconds").Value));
                    opciones.SlidingExpiration = true;
                });

            if (db == "sql")
            {
                services.AddDbContext<SqlDbContext>(
                   options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
                services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
                services.AddScoped<IPostRepositorio, PostRepositorio>();
                services.AddScoped<IInicializadorDB, InicializadorDB>();
            }
            else if (db == "mysql")
            {
                services.AddDbContext<MysqlDbContext>(
                   options => options.UseMySQL(Configuration.GetConnectionString("MysqlConnection")));
                services.AddScoped<IUsuarioRepositorio, INFRASTRUCTURE.DATA.Mysql.Repositories.UsuarioRepositorio>();
                services.AddScoped<IPostRepositorio, INFRASTRUCTURE.DATA.Mysql.Repositories.PostRepositorio>();
                services.AddScoped<IInicializadorDB, INFRASTRUCTURE.DATA.Mysql.Inicializador.InicializadorDB>();
            }

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPostService, PostService>();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IInicializadorDB dbInicial)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Pages/Page404");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            dbInicial.Inicializar();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Posts}/{action=Index}/{id?}");
            });

        }
    }
}
