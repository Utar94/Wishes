using GraphQL.Types;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.GraphQL.Actors;

namespace Logitar.Wishes.GraphQL;

internal abstract class MetadataGraphType<T> : ObjectGraphType<T> where T : Metadata
{
  protected MetadataGraphType(string? description = null)
  {
    Name = typeof(T).Name;
    Description = description;

    Field(x => x.Version)
      .Description("The version of the resource.");

    Field(x => x.CreatedBy, type: typeof(NonNullGraphType<ActorGraphType>))
      .Description("The actor who created the resource.");
    Field(x => x.CreatedOn)
      .Description("The date and time when the resource was created.");

    Field(x => x.UpdatedBy, type: typeof(NonNullGraphType<ActorGraphType>))
      .Description("The actor who updated the resource lastly.");
    Field(x => x.UpdatedOn)
      .Description("The date and time when the resource was updated lastly.");
  }
}
