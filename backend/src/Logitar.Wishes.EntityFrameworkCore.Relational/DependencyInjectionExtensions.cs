using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.Wishes.Application.Actors;
using Logitar.Wishes.Application.Wishlists;
using Logitar.Wishes.Domain.Wishlists;
using Logitar.Wishes.EntityFrameworkCore.Relational.Actors;
using Logitar.Wishes.EntityFrameworkCore.Relational.Queriers;
using Logitar.Wishes.EntityFrameworkCore.Relational.Repositories;
using Logitar.Wishes.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Wishes.EntityFrameworkCore.Relational;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarWishesWithEntityFrameworkCoreRelational(this IServiceCollection services)
  {
    Assembly assembly = typeof(DependencyInjectionExtensions).Assembly;

    return services
      .AddLogitarEventSourcingWithEntityFrameworkCoreRelational()
      .AddLogitarWishesInfrastructure()
      .AddMediatR(config => config.RegisterServicesFromAssembly(assembly))
      .AddQueriers()
      .AddRepositories()
      .AddScoped<IActorService, ActorService>();
  }

  private static IServiceCollection AddQueriers(this IServiceCollection services)
  {
    return services
      .AddScoped<IItemQuerier, ItemQuerier>()
      .AddScoped<IWishlistQuerier, WishlistQuerier>();
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    return services.AddScoped<IWishlistRepository, WishlistRepository>();
  }
}
