using MisTrace.Domain.Entities.RelationDetails;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisTrace.Domain.Entities
{
    public class Trace : Base.BaseEntity
    {
        public Guid GlobalIdentifier { get; set; } = new Guid();
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Guid CreatedById { get; set; }
        public required int OrganizationId { get; set; }
        public virtual ICollection<TraceMilestone> TraceMilestones { get; set; } = [];
        public int? CustomerId { get; set; } = null;

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
