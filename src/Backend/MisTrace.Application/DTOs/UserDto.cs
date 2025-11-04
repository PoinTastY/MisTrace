using System;

namespace MisTrace.Application.DTOs;

public class UserDto
{
    public required int OrganizationId { get; init; }
    public required Guid SubjectGuid { get; init; }
}
