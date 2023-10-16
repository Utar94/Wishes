using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Wishes.GraphQL;

public class WishesSchema : Schema
{
  public WishesSchema(IServiceProvider serviceProvider) : base(serviceProvider)
  {
    Query = serviceProvider.GetRequiredService<RootQuery>();
  }
}
