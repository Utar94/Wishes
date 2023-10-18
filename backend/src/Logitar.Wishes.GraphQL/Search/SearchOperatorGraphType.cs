﻿using GraphQL.Types;
using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.GraphQL.Search;

internal class SearchOperatorGraphType : EnumerationGraphType<SearchOperator>
{
  public SearchOperatorGraphType()
  {
    Name = nameof(SearchOperator);
    Description = "Represents the available operators of a textual search.";

    Add(SearchOperator.And, "All terms must be found for the result to match the search.");
    Add(SearchOperator.Or, "At least one term may be found for the result to match the search.");
  }

  private void Add(SearchOperator value, string description) => Add(value.ToString(), value, description);
}
