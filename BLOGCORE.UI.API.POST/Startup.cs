using BLOGCORE.APPLICATION.Core.DomainServices;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.APPLICATION.Core.Interfaces.Storage;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Data;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Inicializador;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Repositories;
using BLOGCORE.INFRASTRUCTURE.STORAGE;
using BLOGCORE.INFRASTRUCTURE.STORAGE.Services;
using BLOGCORE.UI.API.POST.Parameters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.UI.API.POST
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

            if (db == "sql")
            {
                services.AddDbContext<SqlDbContext>(
                   options => options.UseSqlServer(Configuration.GetConnectionString("ConexionDB")));
                services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
                services.AddScoped<IPostRepositorio, PostRepositorio>();
                services.AddScoped<IInicializadorDB, InicializadorDB>();
            }

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<IComentarioService, ComentarioService>();
            services.AddScoped<IComentarioRepositorio, ComentarioRepositorio>();
            services.AddScoped<IPerfilRepositorio, PerfilRepositorio>();
            services.AddScoped<IPerfilService, PerfilService>();

            if (Configuration["TipoAlmacenamiento"] == "1")
            {
                services.AddScoped<IStorageService, DiskStorageService>();
            }
            else if (Configuration["TipoAlmacenamiento"] == "2")
            {
                //services.AddScoped<IIOService>(provider => new BlobIOService(Configuration.GetConnectionString("BlobStorage")));
                services.AddScoped<IStorageService>(provider => new BlobStorageService(Configuration.GetConnectionString("BlobStorage")));
            }
            else
            {
                services.AddScoped<IStorageService, DiskStorageService>();
            }

            //AUTH
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options =>
            //   {
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           ValidateIssuerSigningKey = true,
            //           IssuerSigningKey = new SymmetricSecurityKey(
            //               Encoding.ASCII.GetBytes(
            //                   ApiParameters.TokenClave
            //                   )),
            //           ValidateIssuer = true,
            //           ValidIssuers = ApiParameters.TokenUrl?.ToLower()?.Split(";"),
            //           ValidateAudience = true,
            //           ValidAudience = ApiParameters.TokenAudience
            //       };
            //   });
            //services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.ASCII.GetBytes(
                               ApiParameters.TokenClave
                               )),

                    ValidateIssuer = true,
                    ValidIssuers = ApiParameters.TokenUrl?.ToLower()?.Split(";"),
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = ApiParameters.TokenAudience
                };
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "MyPolicy",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:4201",
            //                                "http://www.contoso.com")
            //                    .WithMethods("PUT", "DELETE", "GET", "POST");
            //        });
            //});

            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("CorsRule",
            //                  builder =>
            //                  {
            //                      builder.WithOrigins("*");
            //                  });

            //    opt.AddPolicy("CorsRule", rule =>
            //    {
            //        rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*", "localhost", "http://localhost", "http://localhost:4201/");
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IInicializadorDB dbInicial, IConfiguration configuration)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbInicial.Inicializar();
            App_Code.AppCode.InitialVariables(configuration, env);
        }
    }
}
