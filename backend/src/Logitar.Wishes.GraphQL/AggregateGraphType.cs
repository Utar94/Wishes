using Logitar.Wishes.Contracts;

namespace Logitar.Wishes.GraphQL;

internal abstract class AggregateGraphType<T> : MetadataGraphType<T> where T : Aggregate
{
  protected AggregateGraphType(string? description = null) : base(description)
  {
    Field(x => x.Id)
      .Description("The unique identifier of the resource.");
  }
}
