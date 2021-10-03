using Raven.Client.Documents.Session;
using System;
using System.Threading.Tasks;

namespace Marketplace.Framework
{
    public class RavenDbUnitOfWork : IUnitOfWork
    {
        private readonly IAsyncDocumentSession _session;

        public RavenDbUnitOfWork(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public Task Commit()
        {
            return _session.SaveChangesAsync();
        }
    }
}