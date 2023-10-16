export type ContentType = "text/html" | "text/markdown" | "text/plain";

export type Wish = {
  id: string;
  incomplete?: boolean;
  gallery?: string[];
  links?: string[];
  picture: string;
  price?: number[];
  summary?: string;
  title: string;
  contents?: {
    text: string;
    type?: ContentType;
  };
};

export type Wishlist = {
  id: string;
  displayName: string;
  picture: string;
  items: Wish[];
};
