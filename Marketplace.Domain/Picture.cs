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

        public void Resize(PictureSize size) => Apply(new Events.ClassifiedAdPictureResized
        {
            PictureId = Id.Value,
            Height = size.Height,
            Width = size.Width
        });

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.PictureAddedToClassifiedAd e:
                    Id = new PictureId(e.PictureId);
                    Size = new PictureSize(e.Width, e.Height);
                    Location = new Uri(e.Url);
                    Order = e.Order;
                    break;
                case Events.ClassifiedAdPictureResized e:
                    Size = new PictureSize(e.Width, e.Height);
                    break;
            }
        }

        public Picture(Action<object> applier) : base(applier)
        {
        }
    }

    public class PictureId : Value<PictureId>
    {
        public Guid Value { get; }

        public PictureId(Guid value) => Value = value;

    }
}
