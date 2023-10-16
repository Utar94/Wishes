using GraphQL;
using GraphQL.Execution;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Wishes.GraphQL;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarWishesGraphQL(this IServiceCollection services, IConfiguration configuration)
  {
    GraphQLSettings settings = configuration.GetSection("GraphQL").Get<GraphQLSettings>() ?? new();

    return services.AddLogitarWishesGraphQL(settings);
  }

  public static IServiceCollection AddLogitarWishesGraphQL(this IServiceCollection services, GraphQLSettings settings)
  {
    return services.AddGraphQL(builder => builder
      .AddAuthorizationRule()
      .AddSchema<WishesSchema>()
      .AddSystemTextJson()
      .AddErrorInfoProvider(new ErrorInfoProvider(options =>
      {
        options.ExposeExceptionDetails = settings.ExposeExceptionDetails;
      }))
      .AddGraphTypes(typeof(WishesSchema).Assembly)
      .ConfigureExecutionOptions(options =>
      {
        options.EnableMetrics = settings.EnableMetrics;
      }));
  }
}
