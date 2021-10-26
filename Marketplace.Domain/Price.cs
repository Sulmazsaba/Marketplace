using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
    public class Price : Money
    {

        protected Price()
        {
            
        }
        private Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup) : base(amount, currencyCode, currencyLookup)
        {
            if (amount < 0)
                throw new ArgumentException("Price Cannot be negative", nameof(amount));
        }

        internal Price(decimal amount, string currencyCode) : base(amount,new CurrencyDetails(){CurrencyCode = currencyCode})
        {
            if (amount < 0)
                throw new ArgumentException("Price Cannot be negative", nameof(amount));
        }

        public static Price FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) =>
            new Price(amount, currency, currencyLookup);
    }
}
