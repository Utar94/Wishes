﻿using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.Contracts.Wishlists;

public interface IItemService
{
  Task<Item?> ReadAsync(string wishlistId, string itemId, CancellationToken cancellationToken = default);
  Task<AcceptedCommand> RemoveAsync(string wishlistId, string itemId, CancellationToken cancellationToken = default);
  Task<AcceptedCommand> SaveAsync(string wishlistId, SaveItemPayload payload, string? itemId = null, CancellationToken cancellationToken = default);
  Task<SearchResults<Item>> SearchAsync(SearchItemsPayload payload, CancellationToken cancellationToken = default);
  Task<AcceptedCommand> UpdateAsync(string wishlistId, string itemId, UpdateItemPayload payload, CancellationToken cancellationToken = default);
}
