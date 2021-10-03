using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Framework;

namespace Marketplace.Domain
{
    public class Picture : Entity<PictureId>
    {
        internal PictureSize Size { get; set; }
        internal Uri Location { get; set; }
        internal int Order { get; set; }
        protected override void When(object @event)
        {
            
        }

        protected override void EnsureValidState()
        {
           
        }
    }

    public class PictureId : Value<PictureId>
    {
        public Guid Value { get; }

        public PictureId(Guid value) => Value = value;

    }
}
