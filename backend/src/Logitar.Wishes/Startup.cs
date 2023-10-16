using Logitar.Wishes.Extensions;
using Logitar.Wishes.GraphQL;
using Logitar.Wishes.Settings;

namespace Logitar.Wishes;

internal class Startup : StartupBase
{
  private readonly IConfiguration _configuration;
  private readonly bool _enableOpenApi;

  public Startup(IConfiguration configuration)
  {
    _configuration = configuration;
    _enableOpenApi = configuration.GetValue<bool>("EnableOpenApi");
  }

  public override void ConfigureServices(IServiceCollection services)
  {
    base.ConfigureServices(services);

    services
     .AddControllers(/*options => options.Filters.Add<ExceptionHandlingFilter>()*/) // TODO(fpion): ExceptionHandlingFilter
     .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    CorsSettings corsSettings = _configuration.GetSection("Cors").Get<CorsSettings>() ?? new();
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
    //services.AddSingleton<IAuthorizationHandler, PortalActorAuthorizationHandler>(); // TODO(fpion): Authorization

    services.AddLogitarWishesGraphQL(_configuration);

    services.AddApplicationInsightsTelemetry();
    IHealthChecksBuilder healthChecks = services.AddHealthChecks();

    if (_enableOpenApi)
    {
      services.AddOpenApi();
    }

    string connectionString;
    DatabaseProvider databaseProvider = _configuration.GetValue<DatabaseProvider?>("DatabaseProvider")
      ?? DatabaseProvider.EntityFrameworkCorePostgreSQL;
    switch (databaseProvider)
    {
      case DatabaseProvider.EntityFrameworkCorePostgreSQL:
        connectionString = _configuration.GetValue<string>("POSTGRESQLCONNSTR_Wishes") ?? string.Empty;
        //services.AddLogitarWishesWithEntityFrameworkCorePostgreSQL(connectionString);
        //healthChecks.AddDbContextCheck<EventContext>();
        //healthChecks.AddDbContextCheck<WishesContext>();
        break;
      case DatabaseProvider.EntityFrameworkCoreSqlServer:
        connectionString = _configuration.GetValue<string>("SQLCONNSTR_Wishes") ?? string.Empty;
        //services.AddLogitarWishesWithEntityFrameworkCoreSqlServer(connectionString);
        //healthChecks.AddDbContextCheck<EventContext>();
        //healthChecks.AddDbContextCheck<WishesContext>();
        break;
      default:
        throw new DatabaseProviderNotSupportedException(databaseProvider);
    }
  }

  public override void Configure(IApplicationBuilder builder)
  {
    if (_enableOpenApi)
    {
      builder.UseOpenApi();
    }

    if (_configuration.GetValue<bool>("UseGraphQLAltair"))
    {
      builder.UseGraphQLAltair();
    }
    if (_configuration.GetValue<bool>("UseGraphQLGraphiQL"))
    {
      builder.UseGraphQLGraphiQL();
    }
    if (_configuration.GetValue<bool>("UseGraphQLPlayground"))
    {
      builder.UseGraphQLPlayground();
    }
    if (_configuration.GetValue<bool>("UseGraphQLVoyager"))
    {
      builder.UseGraphQLVoyager();
    }

    builder.UseHttpsRedirection();
    builder.UseCors();
    //builder.UseSession(); // TODO(fpion): Session
    //builder.UseAuthentication(); // TODO(fpion): Authentication
    //builder.UseAuthorization(); // TODO(fpion): Authorization

    builder.UseGraphQL<WishesSchema>("/graphql"/*, options => options.AuthenticationSchemes.AddRange(Schemes.All)*/); // TODO(fpion): Authentication

    if (builder is WebApplication application)
    {
      application.MapControllers();
      application.MapHealthChecks("/health");
    }
  }
}
