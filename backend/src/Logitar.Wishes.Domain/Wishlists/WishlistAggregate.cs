using Logitar.EventSourcing;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.Domain.Wishlists.Events;

namespace Logitar.Wishes.Domain.Wishlists;

public class WishlistAggregate : AggregateRoot
{
  private WishlistUpdatedEvent _updated = new();

  public new WishlistId Id => new(base.Id);

  private DisplayNameUnit? _displayName = null;
  public DisplayNameUnit DisplayName
  {
    get => _displayName ?? throw new InvalidOperationException($"The {nameof(DisplayName)} has not been initialized yet.");
    set
    {
      if (value != _displayName)
      {
        _updated.DisplayName = value;
        _displayName = value;
      }
    }
  }
  private Uri? _pictureUrl = null;
  public Uri? PictureUrl
  {
    get => _pictureUrl;
    set
    {
      if (value != _pictureUrl)
      {
        _updated.PictureUrl = new Modification<Uri>(value);
        _pictureUrl = value;
      }
    }
  }

  public WishlistAggregate(AggregateId id) : base(id)
  {
  }

  public WishlistAggregate(DisplayNameUnit displayName, ActorId actorId = default, WishlistId? id = null) : base(id?.AggregateId)
  {
    ApplyChange(new WishlistCreatedEvent(actorId, displayName));
  }
  protected virtual void Apply(WishlistCreatedEvent @event) => _displayName = @event.DisplayName;

  public void Delete(ActorId actorId = default) => ApplyChange(new WishlistDeletedEvent(actorId));

  public void Update(ActorId actorId = default)
  {
    if (_updated.HasChanges)
    {
      _updated.ActorId = actorId;
      ApplyChange(_updated);

      _updated = new();
    }
  }
  protected virtual void Apply(WishlistUpdatedEvent @event)
  {
    if (@event.DisplayName != null)
    {
      _displayName = @event.DisplayName;
    }
    if (@event.PictureUrl != null)
    {
      _pictureUrl = @event.PictureUrl.Value;
    }
  }

  public override string ToString() => $"{DisplayName.Value} | {base.ToString()}";
}
