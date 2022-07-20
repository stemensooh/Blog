using BLOGCORE.APPLICATION.Core.DTOs;
using BLOGCORE.UI.API.POST.Parameters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.API.POST.App_Code
{
    public class AppCode
    {
        public static void InitialConfig(IConfiguration configuration)
        {
            StartupDto.ConnectionStringSQL = configuration["ConnectionStrings:SqlConnection"];
            StartupDto.ConnectionStringMysql = configuration["ConnectionStrings:MysqlConnection"];
            ApiParameters.TokenAudience = configuration["Token:Audience"];
            ApiParameters.TokenUrl = configuration["Token:Url"];
            ApiParameters.TokenClave = configuration["Token:Clave"];
            ApiParameters.TokenDuracion = int.Parse(configuration["Token:Duracion"] ?? "0");
        }

        public static void InitialVariables(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            StartupDto.UploadsFolder = Path.Combine("wwwroot", configuration["DirectorioImagenes"]);
        }
    }
}
