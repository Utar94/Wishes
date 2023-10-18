﻿using Logitar.Wishes.Contracts;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal record DeleteWishlistCommand(string Id) : IRequest<AcceptedCommand>;
