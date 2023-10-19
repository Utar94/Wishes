import type { Aggregate, Metadata } from "./aggregate";

export type ContentType = "text/html" | "text/markdown" | "text/plain";

export type Contents = {
  text: string;
  type: ContentType;
};

export type Item = Metadata & {
  id: string;
  displayName: string;
  summary?: string;
  pictureUrl?: string;
  rank: number;
  rankCategory: number;
  price?: Price;
  contents?: Contents;
  gallery: string[];
  links: string[];
  wishlist: Wishlist;
};

export type Price = {
  average: number;
  minimum: number;
  maximum: number;
  category: number;
};

export type Wishlist = Aggregate & {
  displayName: string;
  pictureUrl?: string;
  itemCount: number;
  items: Item[];
};
