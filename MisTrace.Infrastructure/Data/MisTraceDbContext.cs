using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MisTrace.Domain.Entities;
using MisTrace.Domain.Entities.RelationDetails;

namespace MisTrace.Infrastructure.Data
{
    public class MisTraceDbContext : IdentityDbContext<MisTraceUser>
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Trace> Trace { get; set; }
        public DbSet<TraceMilestone> TraceMilestones { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public MisTraceDbContext(DbContextOptions<MisTraceDbContext> options) : base(options) { }
    }
}
