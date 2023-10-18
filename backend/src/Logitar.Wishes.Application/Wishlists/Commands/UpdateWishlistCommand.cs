using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal record UpdateWishlistCommand(string Id, UpdateWishlistPayload Payload) : IRequest<AcceptedCommand>;
