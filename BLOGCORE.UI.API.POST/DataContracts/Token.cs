
using System;
using System.Text.Json.Serialization;

namespace BLOGCORE.UI.API.POST
{
    public sealed class Token
    {
        public Token()
        {
        }

        public Token(string CodigoError)
        {
            this.CodigoError = CodigoError;
            MensajeRespuesta = ""; // DomainConstants.ObtenerDescripcionError(CodigoError);
        }

        public Token(string CodigoError, string MensajeRespuesta)
        {
            this.CodigoError = CodigoError;
            this.MensajeRespuesta = ""; //  DomainConstants.ObtenerDescripcionError(CodigoError);
            //this.MensajeRespuesta += $" {GSConversions.NothingToString(MensajeRespuesta).Trim()}";
        }

        [JsonPropertyName("token")]
        public string Valor { get; set; }
        public DateTimeOffset? Expedido { get; set; }
        public DateTimeOffset? Expira { get; set; }
        public string CodigoError { get; set; }
        public string MensajeRespuesta { get; set; }
    }
}
