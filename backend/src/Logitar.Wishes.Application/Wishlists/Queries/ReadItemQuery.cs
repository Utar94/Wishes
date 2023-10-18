using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Queries;

internal record ReadItemQuery(string WishlistId, string ItemId) : IRequest<Item?>;
