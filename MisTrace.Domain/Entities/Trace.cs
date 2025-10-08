using MisTrace.Domain.Entities.RelationDetails;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisTrace.Domain.Entities
{
    public class Trace : Base.BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public required virtual MisTraceUser CreatedBy { get; set; }
        public virtual ICollection<TraceMilestone> ServiceMilestones { get; set; } = [];
        public bool IsActive { get; set; } = true;
    }
}
