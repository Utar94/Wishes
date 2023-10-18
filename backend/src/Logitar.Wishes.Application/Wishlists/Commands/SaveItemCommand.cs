using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal record SaveItemCommand(string WishlistId, SaveItemPayload Payload, string? ItemId) : IRequest<AcceptedCommand>;
