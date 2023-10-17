using Logitar.Wishes.Application.Wishlists;
using Logitar.Wishes.Contracts.Wishlists;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Wishes.Application;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarWishesApplication(this IServiceCollection services)
  {
    Assembly assembly = typeof(DependencyInjectionExtensions).Assembly;

    return services
      .AddApplicationServices()
      .AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
  }

  private static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    return services
      .AddTransient<IItemService, ItemService>()
      .AddTransient<IWishlistService, WishlistService>();
  }
}
