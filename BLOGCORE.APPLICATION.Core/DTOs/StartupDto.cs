using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class StartupDto
    {
        public static string ConnectionStringSQL { get; set; }
        public static string ConnectionStringMysql { get; set; }
        public static string UploadsFolder { get; set; }
    }
}
