using BLOGCORE.APPLICATION.Core.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.App_Code
{
    public class AppCode
    {
        public static void InitialConfig(IConfiguration configuration)
        {
            StartupDto.ConnectionStringSQL = configuration["ConnectionStrings:SqlConnection"];
            StartupDto.ConnectionStringMysql = configuration["ConnectionStrings:MysqlConnection"];
        }
    }
}
