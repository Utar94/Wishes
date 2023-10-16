using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.Wishes.EntityFrameworkCore.PostgreSQL;
using Logitar.Wishes.EntityFrameworkCore.Relational;
using Logitar.Wishes.EntityFrameworkCore.SqlServer;
using Logitar.Wishes.Extensions;
using Logitar.Wishes.GraphQL;
using Logitar.Wishes.Web;

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

    services.AddLogitarWishesWeb(_configuration);
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
        services.AddLogitarWishesWithEntityFrameworkCorePostgreSQL(connectionString);
        healthChecks.AddDbContextCheck<EventContext>();
        healthChecks.AddDbContextCheck<WishesContext>();
        break;
      case DatabaseProvider.EntityFrameworkCoreSqlServer:
        connectionString = _configuration.GetValue<string>("SQLCONNSTR_Wishes") ?? string.Empty;
        services.AddLogitarWishesWithEntityFrameworkCoreSqlServer(connectionString);
        healthChecks.AddDbContextCheck<EventContext>();
        healthChecks.AddDbContextCheck<WishesContext>();
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
