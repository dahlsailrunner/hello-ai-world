using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace OpenAi.Sample.Api.StartupServices;

public static class AuthorizationExtensions
{
    public static MvcOptions AddBaseAuthorizationFilters(this MvcOptions options)
    {
        var builder = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
        options.Filters.Add(new AuthorizeFilter(builder.Build()));

        return options;
    }
}