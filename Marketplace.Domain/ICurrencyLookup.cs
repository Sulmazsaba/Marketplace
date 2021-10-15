using Marketplace.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
    public interface ICurrencyLookup
    {
        CurrencyDetails FindCurrency(string currencyCode);
    }

    public class CurrencyDetails : Value<CurrencyDetails>
    {
        public string CurrencyCode { get; set; }
        public bool InUse { get; set; }
        public int DecimalPlaces { get; set; }

        public static CurrencyDetails None = new CurrencyDetails
        {
            InUse = false
        };

    }


}
