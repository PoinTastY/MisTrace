using MisTrace.Domain.Entities.RelationDetails;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisTrace.Domain.Entities
{
    public class Trace : Base.BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Guid CreatedById { get; set; }
        public required int OrganizationId { get; set; }
        public virtual ICollection<TraceMilestone> TraceMilestones { get; set; } = [];
        public bool IsActive { get; set; } = true;
    }
}
