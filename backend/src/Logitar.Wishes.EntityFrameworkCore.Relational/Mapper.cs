using Logitar.EventSourcing;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Actors;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;

namespace Logitar.Wishes.EntityFrameworkCore.Relational;

internal class Mapper
{
  private readonly Dictionary<ActorId, Actor> actors;

  public Mapper()
  {
    actors = new();
  }

  public Mapper(IEnumerable<Actor> actors) : this()
  {
    foreach (Actor actor in actors)
    {
      ActorId id = new(actor.Id);
      this.actors[id] = actor;
    }
  }

  public virtual Actor ToActor(ActorEntity source) => new()
  {
    Id = source.Id,
    Type = source.Type,
    IsDeleted = source.IsDeleted,
    DisplayName = source.DisplayName,
    EmailAddress = source.EmailAddress,
    PictureUrl = source.PictureUrl
  };

  public Wishlist ToWishlist(WishlistEntity source)
  {
    Wishlist destination = new()
    {
      DisplayName = source.DisplayName,
      PictureUrl = source.PictureUrl,
      ItemCount = source.ItemCount
    };

    MapAggregate(source, destination);

    return destination;
  }

  private void MapAggregate(AggregateEntity source, Aggregate destination)
  {
    destination.Id = source.AggregateId;
    destination.Version = source.Version;
    destination.CreatedBy = FindActor(source.CreatedBy);
    destination.CreatedOn = AsUniversalTime(source.CreatedOn);
    destination.UpdatedBy = FindActor(source.UpdatedBy);
    destination.UpdatedOn = AsUniversalTime(source.UpdatedOn);
  }

  private Actor FindActor(string id) => FindActor(new ActorId(id));
  private Actor FindActor(ActorId id) => actors.TryGetValue(id, out Actor? actor) ? actor : new();

  private static DateTime AsUniversalTime(DateTime value) => DateTime.SpecifyKind(value, DateTimeKind.Utc);
}
