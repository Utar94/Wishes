using Logitar.Wishes.Contracts;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal record RemoveItemCommand(string WishlistId, string ItemId) : IRequest<AcceptedCommand>;
