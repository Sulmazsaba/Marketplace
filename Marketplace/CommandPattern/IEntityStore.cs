using System.Threading.Tasks;

namespace Marketplace.Contracts
{
    public interface IEntityStore
    {
        Task<T> Load<T>(string id);
        Task Save<T>(T entity);
    }

   public class RavenDbEntityStore : IEntityStore
    {
        public Task<T> Load<T>(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save<T>(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}