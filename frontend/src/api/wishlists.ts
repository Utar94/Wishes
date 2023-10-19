import type { SearchResults } from "@/types/search";
import type { Wishlist } from "@/types/wishes";
import { get } from ".";

export async function readWishlist(id: string): Promise<Wishlist> {
  return (await get<Wishlist>(`/wishlists/${id}`)).data;
}

export async function searchWishlists(): Promise<SearchResults<Wishlist>> {
  return (await get<SearchResults<Wishlist>>("/wishlists")).data;
}
