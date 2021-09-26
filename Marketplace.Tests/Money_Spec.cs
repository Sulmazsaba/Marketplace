using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Domain;
using Xunit;

namespace Marketplace.Tests
{
    public class Money_Spec
    {
        private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

        [Fact]
        public void Two_of_same_amount_should_be_equal()
        {
            var firstAmount = new Money(5, "EUR", CurrencyLookup);
            var secondAmount = new Money(5, "EUR", CurrencyLookup);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Two_of_same_amount_but_different_currencies_should_not_be_equal()
        {
            var firstAmount = new Money(5, "USD", CurrencyLookup);
            var secondAmount = new Money(5, "EUR", CurrencyLookup);

            Assert.NotEqual(firstAmount, secondAmount);
        }

        [Fact]
        public void FromString_and_fromDecimal_should_be_Equal()
        {
            var firstAmount = Money.FromString("5", "EUR", CurrencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);

            Assert.Equal(firstAmount, secondAmount);

        }

        [Fact]
        public void Sum_of_Money_gives_full_amount()
        {
            var coin1 = new Money(1, "EUR", CurrencyLookup);
            var coin2 = new Money(2, "EUR", CurrencyLookup);
            var coin3 = new Money(2, "EUR", CurrencyLookup);

            var banknote = new Money(5, "EUR", CurrencyLookup);
            Assert.Equal(banknote, coin1 + coin2 + coin3);
        }

        [Fact]
        public void Unused_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(100, "DEM", CurrencyLookup));
        }

        [Fact]
        public void Unknown_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(122, "WHAT??", CurrencyLookup));
        }

        [Fact]
        public void Throw_when_too_many_decimal_places()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Money.FromDecimal(100.233m, "EUR", CurrencyLookup));
        }

        [Fact]
        public void Throws_on_adding_different_currencies()
        {
            var firstAmount = new Money(2, "EUR", CurrencyLookup);
            var secondAmount = new Money(2, "USD", CurrencyLookup);

            Assert.Throws<CurrencyMismatchException>(() => firstAmount + secondAmount);
        }

        [Fact]
        public void Throws_on_subtracting_different_currencies()
        {

            var firstAmount = new Money(2, "EUR", CurrencyLookup);
            var secondAmount = new Money(2, "USD", CurrencyLookup);
            Assert.Throws<CurrencyMismatchException>(() => firstAmount - secondAmount);

        }
    }
}
