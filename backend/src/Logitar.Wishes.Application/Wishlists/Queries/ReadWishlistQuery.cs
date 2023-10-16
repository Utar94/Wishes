using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Queries;

internal record ReadWishlistQuery(string Id) : IRequest<Wishlist?>;
