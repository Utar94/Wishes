using Logitar.EventSourcing.Infrastructure;
using Logitar.Wishes.Application;
using Logitar.Wishes.Application.Caching;
using Logitar.Wishes.Infrastructure.Caching;
using Logitar.Wishes.Infrastructure.Converters;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Logitar.Wishes.Infrastructure;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarWishesInfrastructure(this IServiceCollection services)
  {
    return services
      .AddLogitarEventSourcingInfrastructure()
      .AddLogitarWishesApplication()
      .AddScoped<IEventBus, EventBus>()
      .AddSingleton<ICacheService, CacheService>()
      .AddSingleton<IEventSerializer>(_ => new EventSerializer(GetJsonConverters()));
  }

  private static IEnumerable<JsonConverter> GetJsonConverters() => new JsonConverter[]
  {
    new DisplayNameUnitConverter()
  };
}
