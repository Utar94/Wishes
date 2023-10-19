import type { Item } from "@/types/wishes";
import { get } from ".";

export async function readItem(wishlistId: string, itemId: string): Promise<Item> {
  return (await get<Item>(`/wishlists/${wishlistId}/items/${itemId}`)).data;
}
