using System;
using System.Threading.Tasks;
using Marketplace.Domain;
using Marketplace.Domain.Repositories;
using Raven.Client.Documents.Session;

namespace Marketplace.Infrastructure
{
  public class ClassifiedAdRepository : IClassifiedAdRepository
  {
      private readonly IAsyncDocumentSession _session;

      public ClassifiedAdRepository(IAsyncDocumentSession session)
      {
          _session = session;
      }

      public Task<ClassifiedAd> Load(ClassifiedAdId id)
      {
          return _session.LoadAsync<ClassifiedAd>(EntityId(id));
      }

        public Task Add(ClassifiedAd entity)
        {
            return _session.StoreAsync(entity, EntityId(entity.ClassifiedAdId));
        }

        public Task<bool> Exists(ClassifiedAdId id)
        {
            return _session.Advanced.ExistsAsync(EntityId(id));
        }

        private static string EntityId(ClassifiedAdId id)
        {
            return $"ClassifiedAd/{id.ToString()}";
        }
    }
}
