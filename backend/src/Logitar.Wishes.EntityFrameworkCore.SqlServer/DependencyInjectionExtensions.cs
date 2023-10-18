using Logitar.EventSourcing.EntityFrameworkCore.SqlServer;
using Logitar.Wishes.EntityFrameworkCore.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Wishes.EntityFrameworkCore.SqlServer;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarWishesWithEntityFrameworkCoreSqlServer(this IServiceCollection services, string connectionString)
  {
    return services
      .AddDbContext<WishesContext>(builder => builder.UseSqlServer(connectionString,
        options => options.MigrationsAssembly("Logitar.Wishes.EntityFrameworkCore.SqlServer")
      ))
      .AddLogitarEventSourcingWithEntityFrameworkCoreSqlServer(connectionString)
      .AddLogitarWishesWithEntityFrameworkCoreRelational()
      .AddSingleton<ISqlHelper, SqlServerHelper>();
  }
}
