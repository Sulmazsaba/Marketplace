using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
    public class ClassifiedAd
    {
        public ClassifiedAdId Id { get; }
        private UserId _ownerId { get; set; }
        public ClassifiedAd(ClassifiedAdId id,UserId ownerId)
        {

            Id = id;
            _ownerId = ownerId;
        }

        public void SetTitle(string title) => _title = title;
        public void UpdateText(string text) => _text = text;
        public void UpdatePrice(decimal price) => _price = price;
        private decimal _price { get; set; }
        private string _title { get; set; }
        private string _text { get; set; }
    }
}
