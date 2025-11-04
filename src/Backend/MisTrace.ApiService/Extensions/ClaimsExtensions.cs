using System.Security.Claims;
using MisTrace.Application.DTOs;

namespace MisTrace.ApiService.Extensions;

public static class ClaimsExtensions
{
    public static UserDto BuildUserFromClaims(ClaimsPrincipal user)
    {
        string subClaim = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user.FindFirstValue("sub")
            ?? throw new UnauthorizedAccessException("InvalidClaims");

        Guid sub = new(subClaim);

        string orgClaim = user.FindFirstValue("org")
            ?? throw new UnauthorizedAccessException("InvalidClaims");

        if (!int.TryParse(orgClaim, out int orgId) || orgId < 1)
            throw new UnauthorizedAccessException("InvalidClaims");

        return new UserDto
        {
            SubjectGuid = sub,
            OrganizationId = orgId
        };
    }

}
