using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polly.Retry;

namespace Marketplace.Contracts
{
    public class RetryingCommandHandler<T> : IHandleCommand<T>
    {
        private static RetryPolicy _policy = Policy.Handle<InvalidOperationException>().Retry();
        private IHandleCommand<T> _next;

        public RetryingCommandHandler(IHandleCommand<T> next)
        {
            _next = next;
        }

        public Task Handle(T command)
        {
            return _policy.Execute(() => _next.Handle(command));
        }
    }
}
