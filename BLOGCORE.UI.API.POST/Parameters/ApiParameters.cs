namespace BLOGCORE.UI.API.POST.Parameters
{
    public sealed class ApiParameters
    {
        public static string TokenAudience { get; set; }
        public static string TokenUrl { get; set; }
        public static string TokenClave { get; set; }
        private static int _TokenDuracion;
        public static int TokenDuracion
        {
            get
            {
                return _TokenDuracion;
            }
            set
            {
                if (value <= 0) value = 60;
                _TokenDuracion = value;
            }
        }
    }
}
