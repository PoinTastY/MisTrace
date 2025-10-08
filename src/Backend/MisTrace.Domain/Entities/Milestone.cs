using System.ComponentModel.DataAnnotations.Schema;

namespace MisTrace.Domain.Entities
{
    public class Milestone : Base.BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public required virtual MisTraceUser CreatedBy { get; set; }
        public bool ConcludesService { get; set; } = false;
    }
}
