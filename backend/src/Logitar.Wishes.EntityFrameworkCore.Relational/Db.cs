using Logitar.Data;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;

namespace Logitar.Wishes.EntityFrameworkCore.Relational;

internal static class Db
{
  public static class Actors
  {
    public static readonly TableId Table = new(nameof(WishesContext.Actors));
  }

  public static class Items
  {
    public static readonly TableId Table = new(nameof(WishesContext.Items));
  }

  public static class Wishlists
  {
    public static readonly TableId Table = new(nameof(WishesContext.Wishlists));

    public static readonly ColumnId AggregateId = new(nameof(WishlistEntity.AggregateId), Table);
    public static readonly ColumnId DisplayName = new(nameof(WishlistEntity.DisplayName), Table);
  }
}
