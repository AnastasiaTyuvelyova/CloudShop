using System.Collections.Generic;

namespace Shop.Services
{
    public enum Currencies
    {
        RUR,
        USD,
        EUR
    }

    public class CurrencyService
    {
        private static Dictionary<Currencies, string> currencyCodes = new Dictionary<Currencies, string>()
        {
            { Currencies.RUR, "&#8381" },
            { Currencies.USD, "&#36" },
            { Currencies.EUR, "&#8364" }
        };

        public static string GetCurrencyCode(Currencies currency)
        {
            return currencyCodes[currency];
        }
    }
}
