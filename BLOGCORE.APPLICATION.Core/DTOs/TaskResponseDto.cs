using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.DTOs
{
    public class TaskResponseDto
    {
        public dynamic Data { get; set; }
        public string Mensaje { get; set; }
        public string MensajeError { get; set; }
        public bool TieneError { get; set; }
    }
}
