using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Domain;

namespace Marketplace.Contracts
{
    public class CreateClassifiedAdHandler : IHandleCommand<Contracts.ClassifiedAds.V1.Create>
    {
        private readonly IEntityStore _store;

        public CreateClassifiedAdHandler(IEntityStore store)
        {
            _store = store;
        }

        public Task Handle(ClassifiedAds.V1.Create command)
        {
            var classifiedAd = new ClassifiedAd(new ClassifiedAdId(command.Id), new UserId(command.Id));
            return _store.Save(classifiedAd);
        }
    }
}
