using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
    public static class Events
    {
        public class ClassifiedAdCreated
        {
            public ClassifiedAdId Id { get; set; }
            public Guid OwnerId { get; set; }
        }

        public class ClassifiedAdTitleChanged
        {
            public string Title { get; set; }
            public Guid Id { get; set; }

        }
        
        public class ClassifiedAdTextUpdated
        {
            public string Text { get; set; }
            public Guid Id { get; set; }
        }

        public class ClassifiedAdPriceUpdated
        {
            public Guid Id { get; set; }
            public decimal Price { get; set; }
            public string CurrencyCode { get; set; }
        }

        public class ClassifiedAdSentForReview
        {
            public Guid Id { get; set; }
        }

        public class PictureAddedToClassifiedAd
        {
            public Guid ClassifiedAdId { get; set; }
            public Guid PictureId { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string Url { get; set; }
            public int Order { get; set; }

        }

        public class ClassifiedAdPictureResized
        {
            public Guid PictureId { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
    }

}
