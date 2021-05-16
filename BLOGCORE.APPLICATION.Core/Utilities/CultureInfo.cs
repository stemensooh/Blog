using System.Globalization;

namespace BLOGCORE.APPLICATION.Core.Utilities
{
    public class CultureInfoUtil
    {
        public static void SetCultureInfo()
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
    }
}
