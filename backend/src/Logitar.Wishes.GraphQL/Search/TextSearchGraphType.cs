using GraphQL.Types;
using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.GraphQL.Search;

internal class TextSearchGraphType : InputObjectGraphType<TextSearch>
{
  public TextSearchGraphType()
  {
    Name = nameof(TextSearch);
    Description = "Represents textual search parameters.";

    Field(x => x.Terms, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<SearchTermGraphType>>>))
      .Description("The terms of the textual search.");
    Field(x => x.Operator, type: typeof(NonNullGraphType<SearchOperatorGraphType>))
      .DefaultValue(SearchOperator.And)
      .Description("The operator of the textual search.");
  }
}
