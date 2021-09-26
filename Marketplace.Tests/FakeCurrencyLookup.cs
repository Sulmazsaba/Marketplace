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
        private static readonly IEnumerable<CurrencyDetails> _currencies = new CurrencyDetails[]
        {
            new CurrencyDetails()
            {
                CurrencyCode = "EUR",
                DecimalPlaces = 2,
                InUse = true
            },new CurrencyDetails()
            {
                CurrencyCode = "USD",
                DecimalPlaces = 2,
                InUse = true
            },new CurrencyDetails()
            {
                CurrencyCode = "JPY",
                DecimalPlaces = 0,
                InUse = true
            },new CurrencyDetails()
            {
                CurrencyCode = "DEM",
                DecimalPlaces = 2,
                InUse = false
            }

    };
        public CurrencyDetails FindCurrency(string currencyCode)
        {
            var currency = _currencies.FirstOrDefault(i => i.CurrencyCode == currencyCode);
            return currency?? CurrencyDetails.None;
        }
    }
}
