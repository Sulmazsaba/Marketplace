using System.Threading.Tasks;

namespace Marketplace.Contracts
{
    public interface IHandleCommand<in T>
    {
        Task Handle(T command);
    }
}