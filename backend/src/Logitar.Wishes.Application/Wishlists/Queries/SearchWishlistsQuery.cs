using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Queries;

internal record SearchWishlistsQuery(SearchWishlistsPayload Payload) : IRequest<SearchResults<Wishlist>>;
