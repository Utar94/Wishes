using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal record ReplaceWishlistCommand(string Id, ReplaceWishlistPayload Payload, long? Version) : IRequest<AcceptedCommand>;
