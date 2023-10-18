using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal record CreateWishlistCommand(CreateWishlistPayload Payload) : IRequest<AcceptedCommand>;
