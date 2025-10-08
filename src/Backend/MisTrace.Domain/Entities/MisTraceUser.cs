using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisTrace.Domain.Entities
{
    public class MisTraceUser : IdentityUser
    {
        public int? OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Organization? Organization { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
