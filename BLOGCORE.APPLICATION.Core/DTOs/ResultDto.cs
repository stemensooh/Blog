using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class ResultDto
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public dynamic Errors { get; set; }
        public dynamic Data { get; set; }
    }
}
