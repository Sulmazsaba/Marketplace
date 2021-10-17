using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
    public class ClassifiedAdId : Value<ClassifiedAdId>
    {
        public Guid Value { get; set; }

        public ClassifiedAdId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Classified Ad Id cannot be null");

            Value = value;
        }

        public static implicit operator Guid(ClassifiedAdId self) => self.Value;

        public override string ToString() => Value.ToString();
    }
}
