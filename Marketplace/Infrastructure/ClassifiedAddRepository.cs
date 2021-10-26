using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Domain;
using Marketplace.Domain.Repositories;

namespace Marketplace.Infrastructure
{
    public class ClassifiedAddRepository : IClassifiedAdRepository
    {
        private readonly ClassifiedAdDbContext _dbContext;
        public ClassifiedAddRepository(ClassifiedAdDbContext classifiedAdDbContext) =>
            _dbContext = classifiedAdDbContext;

        public async Task Add(ClassifiedAd entity) => await _dbContext.ClassifiedAds.AddAsync(entity);
        public async Task<ClassifiedAd> Load(ClassifiedAdId id)=> await _dbContext.ClassifiedAds.FindAsync(id.ToString());

        public async Task<bool> Exists(ClassifiedAdId id)
        {
            return await _dbContext.ClassifiedAds.FindAsync(id) == null;
        }
    }
}
