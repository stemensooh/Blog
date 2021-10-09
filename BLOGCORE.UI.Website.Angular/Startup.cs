using BLOGCORE.APPLICATION.Core.DomainServices;
using BLOGCORE.APPLICATION.Core.Interfaces;
using BLOGCORE.APPLICATION.Core.Interfaces.Data;
using BLOGCORE.APPLICATION.Core.Interfaces.Storage;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Data;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Inicializador;
using BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Repositories;
using BLOGCORE.INFRASTRUCTURE.STORAGE;
using BLOGCORE.INFRASTRUCTURE.STORAGE.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.Text;

namespace BLOGCORE.UI.Website.Angular
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
            services.AddControllersWithViews();


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C7q3FBCJZq0bIRRH0Dq4lxWuBipEBkHX"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        //LifetimeValidator = new LifetimeValidator()
                    };
                }
            );

            services.AddCors(opt =>
            {
                //opt.AddPolicy("CorsRule",
                //              builder =>
                //              {
                //                  builder.WithOrigins("*");
                //              });

                opt.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost", "*");
                });
            });

            BLOGCORE.UI.Website.Angular.Utilities.Constants.ExpiracionMinutos = int.Parse(Configuration["ExpiracionMinutos"]);

            {
                CultureInfo forceDotCulture;
                forceDotCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                forceDotCulture.NumberFormat.CurrencySymbol = "$";
                forceDotCulture.NumberFormat.NumberDecimalSeparator = ",";
                forceDotCulture.NumberFormat.CurrencyDecimalSeparator = ",";
                forceDotCulture.NumberFormat.NumberGroupSeparator = ".";
                forceDotCulture.NumberFormat.CurrencyGroupSeparator = ".";
                forceDotCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                CultureInfo.DefaultThreadCurrentCulture = forceDotCulture;
                CultureInfo.DefaultThreadCurrentUICulture = forceDotCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = forceDotCulture;
            }

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











            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IInicializadorDB dbInicial, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseCors("CorsRule");
            app.UseAuthentication();
            app.UseAuthorization();

            dbInicial.Inicializar();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.StartupTimeout = new TimeSpan(0, 1, 0);
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            App_Code.AppCode.InitialVariables(configuration, env);
        }
    }
}
