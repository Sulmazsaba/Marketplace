using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
   public class Money
    {
        public decimal Amount { get; }
        public Money(decimal amount) => Amount = amount;
    }
}
