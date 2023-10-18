using GraphQL.Types;
using Logitar.Wishes.Contracts.Actors;

namespace Logitar.Wishes.GraphQL.Actors;

internal class ActorTypeGraphType : EnumerationGraphType<ActorType>
{
  public ActorTypeGraphType()
  {
    Name = nameof(ActorType);
    Description = "Represents the available actor types.";

    Add(ActorType.ApiKey, "The actor is an API key.");
    Add(ActorType.System, "The actor is the system.");
    Add(ActorType.User, "The actor is an user.");
  }

  private void Add(ActorType value, string description) => Add(value.ToString(), value, description);
}
