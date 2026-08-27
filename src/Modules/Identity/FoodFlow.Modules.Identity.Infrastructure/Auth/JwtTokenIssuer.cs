using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FoodFlow.BuildingBlocks.Authentication;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users;
using FoodFlow.Modules.Identity.Domain.Auth;
using FoodFlow.Modules.Identity.Domain.Auth.Contracts;
using Microsoft.Extensions.Options;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FoodFlow.Modules.Identity.Infrastructure.Auth;

internal sealed class JwtTokenIssuer(
    RsaSigningKeyProvider keyProvider,
    IOptions<JwtOptions> options,
    TimeProvider timeProvider) : IJwtTokenIssuer
{
    private readonly JwtOptions _options = options.Value;

    public IssuedAccessToken IssueAccessToken(User user, Guid sessionId)
    {
        ArgumentNullException.ThrowIfNull(user);

        var now = timeProvider.GetUtcNow();
        var expiresAt = now.AddMinutes(_options.AccessTokenLifetimeMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.PreferredUsername, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.Name.FullName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("sid", sessionId.ToString())
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim("role", role.Name));
        }

        foreach (var permission in user.Roles.SelectMany(r => r.Permissions))
        {
            claims.Add(new Claim("permission", permission.Name));
        }

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: now.UtcDateTime,
            expires: expiresAt.UtcDateTime,
            signingCredentials: keyProvider.SigningCredentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return new IssuedAccessToken(jwt, expiresAt);
    }
}
