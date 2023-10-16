using Logitar.Portal.Client;
using Logitar.Wishes.Application;
using Logitar.Wishes.Contracts.Constants;
using Logitar.Wishes.Web.Authentication;
using Logitar.Wishes.Web.Authorization;
using Logitar.Wishes.Web.Extensions;
using Logitar.Wishes.Web.Filters;
using Logitar.Wishes.Web.Settings;
using Microsoft.AspNetCore.Authorization;

namespace Logitar.Wishes.Web;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarWishesWeb(this IServiceCollection services, IConfiguration configuration)
  {
    services
     .AddControllers(options => options.Filters.Add<ExceptionHandlingFilter>())
     .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    CorsSettings corsSettings = configuration.GetSection("Cors").Get<CorsSettings>() ?? new();
    services.AddSingleton(corsSettings);
    services.AddCors(corsSettings);

    IdentitySettings identitySettings = configuration.GetSection("Identity").Get<IdentitySettings>() ?? new();
    services.AddSingleton(identitySettings);

    services.AddAuthentication()
      .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(Schemes.ApiKey, options => { })
      .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(Schemes.Basic, options => { });

    services.AddAuthorization(options =>
    {
      options.AddPolicy(Policies.CanReadWishlists, new AuthorizationPolicyBuilder(Schemes.All)
        .RequireAuthenticatedUser()
        .AddRequirements(new RoleAuthorizationRequirement(identitySettings.Roles.ReadWishlists))
        .Build());
      options.AddPolicy(Policies.CanWriteWishlists, new AuthorizationPolicyBuilder(Schemes.All)
        .RequireAuthenticatedUser()
        .AddRequirements(new RoleAuthorizationRequirement(identitySettings.Roles.WriteWishlists))
        .Build());
    });

    services.AddMemoryCache();
    services.AddSingleton<IApplicationContext, HttpApplicationContext>();
    services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();

    services.AddLogitarPortalClient(identitySettings);

    return services;
  }
}
