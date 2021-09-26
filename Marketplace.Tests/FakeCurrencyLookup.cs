using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Tests
{
    public class FakeCurrencyLookup : ICurrencyLookup
    {
        private static readonly IEnumerable<CurrencyDetails> Currencies = new CurrencyDetails[]
        {
            new CurrencyDetails()
            {
                CurrencyCode = "EUR",
                DecimalPlaces = 2,
                InUse = true
            },

        };
        public CurrencyDetails FindCurrency(string currencyCode)
        {
            throw new NotImplementedException();
        }
    }
}
