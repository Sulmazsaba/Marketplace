using System;

namespace Marketplace.Domain
{
    public class FixedCurrencyLookup : ICurrencyLookup
    {
        public CurrencyDetails FindCurrency(string currencyCode)
        {
            throw new NotImplementedException();
        }
    }
}