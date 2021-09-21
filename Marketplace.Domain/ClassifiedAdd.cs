using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
   public class ClassifiedAdd
    {
        public Guid Id { get; private set; }


        private Guid _ownerId { get; set; }
        private decimal _price { get; set; }
        private string _title { get; set; }

        private string _text { get; set; }
    }
}
