﻿using GraphQL.Types;
using Logitar.Wishes.GraphQL.Wishlists;

namespace Logitar.Wishes.GraphQL;

internal class RootQuery : ObjectGraphType
{
  public RootQuery()
  {
    Name = nameof(RootQuery);

    ItemQueries.Register(this);
    WishlistQueries.Register(this);
  }
}
