using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Framework;

namespace Marketplace.Infrastructure
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly ClassifiedAdDbContext _classifiedAdDbContext;

        public EfCoreUnitOfWork(ClassifiedAdDbContext classifiedAdDbContext)
        {
            _classifiedAdDbContext = classifiedAdDbContext;
        }

        public Task Commit()
        {
           return _classifiedAdDbContext.SaveChangesAsync();
        }
    }
}
