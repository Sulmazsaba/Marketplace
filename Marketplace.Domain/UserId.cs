using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
    // value object
    public class UserId
    {
        private readonly Guid _value;
        public UserId(Guid value) =>  _value = value;
    }
}
