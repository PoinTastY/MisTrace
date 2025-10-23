using Microsoft.EntityFrameworkCore;
using MisTrace.Domain.Entities;
using MisTrace.Domain.Entities.RelationDetails;

namespace MisTrace.Infrastructure.Data
{
    public class MisTraceDbContext : DbContext
    {
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Trace> Trace { get; set; }
        public DbSet<TraceMilestone> TraceMilestones { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public MisTraceDbContext(DbContextOptions<MisTraceDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("mistrace");
        }
    }
}
