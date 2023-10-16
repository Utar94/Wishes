﻿using GraphQL.Types;
using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.GraphQL.Search;

internal abstract class SearchPayloadInputGraphType<T> : InputObjectGraphType<T> where T : SearchPayload
{
  public SearchPayloadInputGraphType()
  {
    Name = typeof(T).Name;
    Description = "Represents the parameters of a search.";

    Field(x => x.Id, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<StringGraphType>>>))
      .DefaultValue(new TextSearch())
      .Description("The unique identifier list filter to apply.");
    Field(x => x.Search, type: typeof(NonNullGraphType<TextSearchGraphType>))
      .DefaultValue(new TextSearch())
      .Description("The global textual search parameters to apply.");

    Field(x => x.Skip).DefaultValue(0)
      .Description("The minimum number of results to skip.");
    Field(x => x.Limit).DefaultValue(0)
      .Description("The maximum number of results to return.");
  }
}
