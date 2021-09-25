using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
    public class Money : Value<Money>
    {
        private const string DefaultCurrency = "EUR";

        public static Money FromDecimal(decimal amount) => new Money(amount);
        public static Money FromString(string amount) => new Money(decimal.Parse(amount));
        protected Money(decimal amount, string currencyCode = "EUR")
        {
            if (decimal.Round(amount, 2) != amount)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than two decimals");
            Amount = amount;
            CurrencyCode = currencyCode;
        }
        public decimal Amount { get; }
        public string CurrencyCode { get; }
        public Money Add(Money summand)
        {
            if (CurrencyCode != summand.CurrencyCode)
                throw new CurrencyMismatchExeption("Cannot sum amount with different currencies");
            return new Money(Amount + summand.Amount);
        }
        public Money Substract(Money subrtrahend)
        {
            if (CurrencyCode != subrtrahend.CurrencyCode)
                throw new CurrencyMismatchExeption("Cannot sum amount with different currencies");
            return new Money(Amount - subrtrahend.Amount);
        }
        public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);
        public static Money operator -(Money minuend, Money subtrahend) => minuend.Substract(subtrahend);
    }

    public class CurrencyMismatchExeption : Exception
    {
        public CurrencyMismatchExeption(string message) : base(message)
        {

        }
    }
}
