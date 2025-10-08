using System.ComponentModel.DataAnnotations.Schema;

namespace MisTrace.Domain.Entities.RelationDetails
{
    public class TraceMilestone : Base.BaseEntity
    {
        public int TraceId { get; set; }
        [ForeignKey("TraceId")]
        public virtual Trace Trace { get; set; } = null!;
        public int MilestoneId { get; set; }
        [ForeignKey("MilestoneId")]
        public virtual Milestone Milestone { get; set; } = null!;
    }
}
