using Logitar.EventSourcing;
using Logitar.Wishes.Contracts.Actors;
using Logitar.Wishes.Domain.Validators;
using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Configurations;

internal class ActorConfiguration : IEntityTypeConfiguration<ActorEntity>
{
  public void Configure(EntityTypeBuilder<ActorEntity> builder)
  {
    builder.ToTable(Db.Actors.Table.Table!, Db.Actors.Table.Schema);
    builder.HasKey(x => x.ActorId);

    builder.HasIndex(x => x.Id).IsUnique();
    builder.HasIndex(x => x.Type);
    builder.HasIndex(x => x.IsDeleted);
    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.EmailAddress);

    builder.Property(x => x.Id).IsRequired().HasMaxLength(AggregateId.MaximumLength);
    builder.Property(x => x.Type).HasMaxLength(byte.MaxValue).HasConversion(new EnumToStringConverter<ActorType>());
    builder.Property(x => x.DisplayName).HasMaxLength(DisplayNameUnit.MaximumLength);
    builder.Property(x => x.EmailAddress).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.PictureUrl).HasMaxLength(UrlValidator.MaximumLength);
  }
}
