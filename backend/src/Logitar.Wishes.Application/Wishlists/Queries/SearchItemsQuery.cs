using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Queries;

internal record SearchItemsQuery(SearchItemsPayload Payload) : IRequest<SearchResults<Item>>;
