using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace Marketplace.Infrastructure
{
    public class ClassifiedAdDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public ClassifiedAdDbContext(DbContextOptions<ClassifiedAdDbContext> options ,ILoggerFactory loggerFactory) : base(options)
        {
            this._loggerFactory = loggerFactory;
        }

        public  DbSet<ClassifiedAd> ClassifiedAds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            //disable in production mode
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClassifiedEntityTypeConfiguration());
        }


    }

    public class ClassifiedEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
    {
        public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
        {
             builder.HasKey(x => x.ClassifiedAdId);
        }
    }
}
