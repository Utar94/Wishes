using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Wishes.EntityFrameworkCore.Relational;

public class WishesContext : DbContext
{
  public WishesContext(DbContextOptions<WishesContext> options) : base(options)
  {
  }

  internal DbSet<ActorEntity> Actors { get; private set; }
  internal DbSet<WishlistEntity> Wishlists { get; private set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(WishesContext).Assembly);
  }
}
