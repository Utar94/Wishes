using Logitar.EventSourcing.EntityFrameworkCore.PostgreSQL;
using Logitar.Wishes.EntityFrameworkCore.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Wishes.EntityFrameworkCore.PostgreSQL;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarWishesWithEntityFrameworkCorePostgreSQL(this IServiceCollection services, string connectionString)
  {
    return services
      .AddDbContext<WishesContext>(builder => builder.UseNpgsql(connectionString,
        options => options.MigrationsAssembly("Logitar.Wishes.EntityFrameworkCore.PostgreSQL")
      ))
      .AddLogitarEventSourcingWithEntityFrameworkCorePostgreSQL(connectionString)
      .AddLogitarWishesWithEntityFrameworkCoreRelational()
      .AddSingleton<ISqlHelper, PostgresHelper>();
  }
}
