using Logitar.EventSourcing;
using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.Domain.Wishlists;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Configurations;

internal class ItemConfiguration : IEntityTypeConfiguration<ItemEntity>
{
  public void Configure(EntityTypeBuilder<ItemEntity> builder)
  {
    int contentTypeMaximumLength = ContentTypeValidator.SupportedTypes.Max(x => x.Length);

    builder.ToTable(Db.Items.Table.Table!, Db.Items.Table.Schema);
    builder.HasKey(x => x.ItemId);

    builder.HasIndex(x => new { x.WishlistId, x.Id }).IsUnique();
    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.Summary);
    builder.HasIndex(x => x.Rank);
    builder.HasIndex(x => x.RankCategory);
    builder.HasIndex(x => x.AveragePrice);
    builder.HasIndex(x => x.MinimumPrice);
    builder.HasIndex(x => x.MaximumPrice);
    builder.HasIndex(x => x.PriceCategory);
    builder.HasIndex(x => x.Version);
    builder.HasIndex(x => x.CreatedBy);
    builder.HasIndex(x => x.CreatedOn);
    builder.HasIndex(x => x.UpdatedBy);
    builder.HasIndex(x => x.UpdatedOn);

    builder.HasOne(x => x.Wishlist).WithMany(x => x.Items).OnDelete(DeleteBehavior.Cascade);

    builder.Ignore(x => x.Gallery);
    builder.Ignore(x => x.Links);

    builder.Property(x => x.Id).IsRequired().HasMaxLength(ItemId.MaximumLength);
    builder.Property(x => x.DisplayName).HasMaxLength(DisplayNameUnit.MaximumLength);
    builder.Property(x => x.Summary).HasMaxLength(SummaryUnit.MaximumLength);
    builder.Property(x => x.PictureUrl).HasMaxLength(UrlUnit.MaximumLength);
    builder.Property(x => x.ContentType).HasMaxLength(contentTypeMaximumLength);
    builder.Property(x => x.GallerySerialized).HasColumnName(nameof(ItemEntity.Gallery));
    builder.Property(x => x.LinksSerialized).HasColumnName(nameof(ItemEntity.Links));
    builder.Property(x => x.CreatedBy).HasMaxLength(ActorId.MaximumLength);
    builder.Property(x => x.UpdatedBy).HasMaxLength(ActorId.MaximumLength);
  }
}
