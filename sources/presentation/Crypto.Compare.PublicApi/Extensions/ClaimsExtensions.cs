using System.Security.Claims;

namespace Crypto.Compare.PublicApi.Extensions;

public static class ClaimsExtensions
{
    public static long? UserId(this ClaimsPrincipal identity)
    {
        return !long.TryParse(identity.GetClaim("user_id")?.Value, out var result) ? new long?() : result;
    }

    public static Claim? GetClaim(this ClaimsPrincipal identity, string claimName) =>
        identity.Claims.FirstOrDefault<Claim>((Func<Claim?, bool>)(c => c?.Type == claimName));
}
