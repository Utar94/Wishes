using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal record UpdateItemCommand(string WishlistId, string ItemId, UpdateItemPayload Payload) : IRequest<AcceptedCommand>;
