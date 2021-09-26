using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Framework;

namespace Marketplace.Domain
{
    public class Money : Value<Money>
    {
        private const string DefaultCurrency = "EUR";

        public static Money FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) =>
            new Money(amount, currency, currencyLookup);
        public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup) =>
            new Money(decimal.Parse(amount), currency, currencyLookup);
        public Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            if (string.IsNullOrEmpty(currencyCode))
                throw new ArgumentNullException(nameof(currencyCode), "currency code must be specified");
            var currency = currencyLookup.FindCurrency(currencyCode);
            if (!currency.InUse)
                throw new ArgumentException($"Currency {currencyCode} is not valid");


            if (decimal.Round(amount, currency.DecimalPlaces) != amount)
                throw new ArgumentOutOfRangeException(nameof(amount), $"Amount in {currencyCode} cannot have more than {currency.DecimalPlaces} decimals");
            Amount = amount;
            Currency= currency;
        }
        protected Money(decimal amount,CurrencyDetails currency)
        {
            Amount = amount;
            Currency = currency; 
            
        }
        

        public decimal Amount { get; }
        public CurrencyDetails Currency { get; }
        public Money Add(Money summand)
        {
            if (Currency != summand.Currency)
                throw new CurrencyMismatchException("Cannot sum amount with different currencies");
            return new Money(Amount + summand.Amount,Currency);
        }
        public Money Substract(Money subrtrahend)
        {
            if (Currency != subrtrahend.Currency)
                throw new CurrencyMismatchException("Cannot sum amount with different currencies");
            return new Money(Amount - subrtrahend.Amount,Currency);
        }
        public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);
        public static Money operator -(Money minuend, Money subtrahend) => minuend.Substract(subtrahend);
    }

    public class CurrencyMismatchException : Exception
    {
        public CurrencyMismatchException(string message) : base(message)
        {

        }
    }
}
