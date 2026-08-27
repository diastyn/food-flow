using System.Security.Claims;
using FoodFlow.BuildingBlocks.Domain.Authentication;
using Microsoft.AspNetCore.Http;

namespace FoodFlow.BuildingBlocks.Authentication;

public sealed class HttpRequestContext(
    IHttpContextAccessor httpContextAccessor) : IRequestContext
{
    public Guid? UserId
    {
        get
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
            {
                return null;
            }

            var user = httpContext.User;
            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var sub = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue("sub");
            return Guid.TryParse(sub, out var id) ? id : null;
        }
    }

    public string? IpAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

    public string? UserAgent => httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString();
}
