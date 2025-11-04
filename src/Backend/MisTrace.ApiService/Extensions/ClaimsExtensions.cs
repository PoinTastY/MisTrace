using System;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using MisTrace.Application.DTOs;

namespace MisTrace.ApiService.Extensions;

public static class ClaimsExtensions
{
    public static UserDto BuildUserFromClaims(ClaimsPrincipal user)
    {
        string subClaim = user.FindFirstValue("sub")
            ?? throw new UnauthorizedAccessException("InvalidClaims");

        Guid sub = new(subClaim);

        string orgClaim = user.FindFirstValue("org")
            ?? throw new UnauthorizedAccessException("InvalidClaims");

        if (!int.TryParse(orgClaim, out int orgId))
            throw new UnauthorizedAccessException("InvalidClaims");

        if (orgId < 1)
            throw new UnauthorizedAccessException("InvalidClaims");

        return new UserDto
        {
            SubjectGuid = sub,
            OrganizationId = orgId
        };
    }
}
