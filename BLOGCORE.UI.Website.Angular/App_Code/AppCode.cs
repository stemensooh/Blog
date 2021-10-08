using BLOGCORE.APPLICATION.Core.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Angular.App_Code
{
    public class AppCode
    {
        public static void InitialConfig(IConfiguration configuration)
        {
            StartupDto.ConnectionStringSQL = configuration["ConnectionStrings:SqlConnection"];
            StartupDto.ConnectionStringMysql = configuration["ConnectionStrings:MysqlConnection"];
        }

        public static void InitialVariables(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            StartupDto.UploadsFolder = Path.Combine("wwwroot", configuration["DirectorioImagenes"]);
        }
    }
}
