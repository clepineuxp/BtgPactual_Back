using BtgPactual.Back.Domain.Constants;
using System.Globalization;

namespace BtgPactual.Back.Core.Helpers
{
    public static class StringHelpers
    {
        public static string FormatToColombianCurrency(this string amount)
        {
            if (double.TryParse(amount, out double decimalValue))
            {
                CultureInfo colombianCulture = new CultureInfo("es-CO");
                return string.Format(colombianCulture, "{0:C}", decimalValue);
            }
            return amount;
        }
        public static string FormatToColombianCurrency(this double amount)
        {
            CultureInfo colombianCulture = new CultureInfo("es-CO");
            return string.Format(colombianCulture, "{0:C}", amount);
        }
        public static string FormatDateTime(this DateTime date)
        {
            return date.ToString(Constants.Formats.DateTime);
        }
    }
}
