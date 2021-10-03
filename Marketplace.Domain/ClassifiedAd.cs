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
        public ClassifiedAdId Id { get; private set; }
        public UserId OwnerId { get; private set; }
        public Price Price { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public ClassifiedAdState State { get; private set; }
        public UserId ApprovedBy { get; private set; }

        public ClassifiedAd(ClassifiedAdId id, UserId ownerId) =>
            Apply(new Events.ClassifiedAdCreated()
            {
                Id = id,
                OwnerId = ownerId
            });

        public void SetTitle(ClassifiedAdTitle title)=>
            Apply(new Events.ClassifiedAdTitleChanged()
            {
                Id = Id,
                Title = title
            });

        public void UpdateText(ClassifiedAdText text)=>
            Apply(new Events.ClassifiedAdTextUpdated()
            {
                Id = Id,
                Text = text
            });
        public void UpdatePrice(Price price)=>
            Apply(new Events.ClassifiedAdPriceUpdated()
            {
                Id = Id,
                CurrencyCode = Price.Currency.CurrencyCode,
                Price = Price.Amount
            });

        public void RequestToPublish() =>
            Apply(new Events.ClassifiedAdSentForReview()
            {
                Id = Id
            });
        protected override void EnsureValidState()
        {
            var valid = Id != null && OwnerId != null && (State switch
            {
                ClassifiedAdState.PendingReview => Text != null && Title != null && Price?.Amount > 0,
                ClassifiedAdState.Active => Text != null && Title != null && Price?.Amount > 0 && ApprovedBy != null,
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
                    Id = new ClassifiedAdId(e.Id);
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;
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
