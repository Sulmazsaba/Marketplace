using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Domain.Exceptions;
using Marketplace.Framework;

namespace Marketplace.Domain
{
    public class ClassifiedAd : AggregateRoot<ClassifiedAdId>
    {
        internal ClassifiedAd()
        {
            
        }
        private string DbId
        {
            get => $"ClassifiedAd/{ClassifiedAdId}";
            set { }
        }
        public Guid ClassifiedAdId { get; private set; }
        public UserId OwnerId { get; private set; }
        public Price Price { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public ClassifiedAdState State { get; private set; }
        public UserId ApprovedBy { get; private set; }
        public List<Picture> Pictures { get; private set; }

        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            Pictures = new List<Picture>();
            Apply(new Events.ClassifiedAdCreated()
            {
                Id = id,
                OwnerId = ownerId
            });
        }

        public void SetTitle(ClassifiedAdTitle title) =>
            Apply(new Events.ClassifiedAdTitleChanged()
            {
                Id = ClassifiedAdId,
                Title = title
            });

        public void UpdateText(ClassifiedAdText text) =>
            Apply(new Events.ClassifiedAdTextUpdated()
            {
                Id = ClassifiedAdId,
                Text = text
            });
        public void UpdatePrice(Price price) =>
            Apply(new Events.ClassifiedAdPriceUpdated()
            {
                Id = ClassifiedAdId,
                CurrencyCode = Price.Currency.CurrencyCode,
                Price = Price.Amount
            });

        public void RequestToPublish() =>
            Apply(new Events.ClassifiedAdSentForReview()
            {
                Id = ClassifiedAdId
            });

        public void AddPicture(PictureSize size, Uri pictureUri) =>
            Apply(new Events.PictureAddedToClassifiedAd
            {
                ClassifiedAdId = ClassifiedAdId,
                Width = size.Width,
                Height = size.Height,
                Url = pictureUri.ToString(),
                PictureId = new Guid(),
                Order = Pictures.Max(i => i.Order)

            });

        public void ResizePicture(PictureId pictureId, PictureSize pictureSize)
        {
            var picture = FindPicture(pictureId);
            if (picture == null)
                throw new InvalidOperationException("Cannot resize a picture that I don't have");
            picture.Resize(pictureSize);
        }

        private Picture FindPicture(PictureId id) => Pictures.FirstOrDefault(i => i.Id == id);

        private Picture FirstPicture => Pictures.OrderBy(i => i.Order).FirstOrDefault();

        protected override void EnsureValidState()
        {
            var valid = ClassifiedAdId != null &&
                        OwnerId != null &&
                        (State switch
                        {
                            ClassifiedAdState.PendingReview =>
                                Text != null &&
                                Title != null &&
                                Price?.Amount > 0 &&
                            FirstPicture.HasCorrectSize(),
                            ClassifiedAdState.Active =>
                                Text != null &&
                                Title != null &&
                                Price?.Amount > 0 &&
                                ApprovedBy != null &&
                                FirstPicture.HasCorrectSize(),
                            _ => true
                        });

            if (!valid)
                throw new InvalidEntityStateException(this, $"Post-checks failed in state {State}");

        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.ClassifiedAdCreated e:
                    ClassifiedAdId = new ClassifiedAdId(e.Id);
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;
                    ClassifiedAdId = e.Id;
                    break;
                case Events.ClassifiedAdTitleChanged e:
                    Title = new ClassifiedAdTitle(e.Title);
                    break;
                case Events.ClassifiedAdTextUpdated e:
                    Text = new ClassifiedAdText(e.Text);
                    break;
                case Events.ClassifiedAdPriceUpdated e:
                    Price = new Price(e.Price, e.CurrencyCode);
                    break;
                case Events.ClassifiedAdSentForReview e:
                    State = ClassifiedAdState.PendingReview;
                    break;
                case Events.PictureAddedToClassifiedAd e:
                    var picture = new Picture(Apply);
                    ApplyToEntity(picture, e);
                    Pictures.Add(picture);
                    break;

            }
        }

        public enum ClassifiedAdState
        {
            PendingReview,
            Active,
            Inactive,
            MarkedAsSold
        }
    }
}
