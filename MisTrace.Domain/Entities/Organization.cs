using MisTrace.Domain.Entities.RelationDetails;

namespace MisTrace.Domain.Entities
{
    public class Organization : Base.BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; } = null;
        public virtual ICollection<OrganizationUser> Users { get; set; } = [];
    }
}
