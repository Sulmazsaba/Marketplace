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
        public  Guid Value { get; set; }
        public UserId(Guid value) =>  Value = value;

        public static implicit operator Guid(UserId self) => self.Value;

        public override string ToString() => Value.ToString();
    }
}
