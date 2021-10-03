using System.Threading.Tasks;

namespace Marketplace.Domain.Repositories
{
   public interface IClassifiedAdRepository
   {
       Task<ClassifiedAd> Load(ClassifiedAdId id);
       Task Add(ClassifiedAd entity);
       Task<bool> Exists(ClassifiedAdId id);
   }
}
