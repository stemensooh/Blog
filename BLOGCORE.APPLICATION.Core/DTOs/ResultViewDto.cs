using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class ResultViewDto
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public dynamic Data { get; set; }
    }
}
