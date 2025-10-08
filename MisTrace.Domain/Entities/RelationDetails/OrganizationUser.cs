using System.ComponentModel.DataAnnotations.Schema;

namespace MisTrace.Domain.Entities.RelationDetails
{
    public class OrganizationUser : Base.BaseEntity
    {
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; } = null!;
        public string UserId { get; set; } = null!;
        [ForeignKey("UserId")]
        public virtual MisTraceUser User { get; set; } = null!;
        public bool IsOwner { get; set; } = false;
    }
}
