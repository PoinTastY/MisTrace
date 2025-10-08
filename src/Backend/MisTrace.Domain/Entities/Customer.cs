using System.ComponentModel.DataAnnotations.Schema;

namespace MisTrace.Domain.Entities
{
    public class Customer : Base.BaseEntity
    {
        public required string Name { get; set; }
        public string? AddressHash { get; set; }
        public string? PhoneNumberHash { get; set; }
        public required string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public required virtual MisTraceUser CreatedBy { get; set; }
    }
}
