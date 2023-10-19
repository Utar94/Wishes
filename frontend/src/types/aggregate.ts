import type { Actor } from "./actor";

export type Aggregate = Metadata & {
  id: string;
};

export type Metadata = {
  createdBy: Actor;
  createdOn: string;
  updatedBy: Actor;
  updatedOn: string;
  version: number;
};
