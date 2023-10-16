using Logitar.Wishes.Application;
using Logitar.Wishes.Web.Extensions;
using Logitar.Wishes.Web.Filters;
using Logitar.Wishes.Web.Settings;

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

    //services.AddAuthentication()
    //  .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(Schemes.ApiKey, options => { })
    //  .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(Schemes.Basic, options => { })
    //  .AddScheme<SessionAuthenticationOptions, SessionAuthenticationHandler>(Schemes.Session, options => { }); // TODO(fpion): Authentication

    //services.AddAuthorization(options =>
    //{
    //  options.AddPolicy(Policies.PortalActor, new AuthorizationPolicyBuilder(Schemes.All)
    //  .RequireAuthenticatedUser()
    //    .AddRequirements(new PortalActorAuthorizationRequirement())
    //    .Build());
    //}); // TODO(fpion): Authorization

    //CookiesSettings cookiesSettings = configuration.GetSection("Cookies").Get<CookiesSettings>() ?? new();
    //services.AddSingleton(cookiesSettings);
    //services.AddSession(options =>
    //{
    //  options.Cookie.SameSite = cookiesSettings.Session.SameSite;
    //  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //}); // TODO(fpion): Session

    //services.AddDistributedMemoryCache(); // TODO(fpion): Session
    services.AddMemoryCache();
    services.AddSingleton<IApplicationContext, HttpApplicationContext>();
    //services.AddSingleton<IAuthorizationHandler, PortalActorAuthorizationHandler>(); // TODO(fpion): Authorization

    return services;
  }
}
