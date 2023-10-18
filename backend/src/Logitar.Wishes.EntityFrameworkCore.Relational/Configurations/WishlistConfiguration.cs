using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Configurations;

internal class WishlistConfiguration : AggregateConfiguration<WishlistEntity>, IEntityTypeConfiguration<WishlistEntity>
{
  public override void Configure(EntityTypeBuilder<WishlistEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(Db.Wishlists.Table.Table!, Db.Wishlists.Table.Schema);
    builder.HasKey(x => x.WishlistId);

    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.ItemCount);

    builder.Property(x => x.DisplayName).HasMaxLength(DisplayNameUnit.MaximumLength);
    builder.Property(x => x.PictureUrl).HasMaxLength(UrlUnit.MaximumLength);
  }
}
