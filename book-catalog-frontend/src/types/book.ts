export interface Book {
  isbn: string;
  title: string;
  author: string;
  ownership: Ownership;
}

export interface BooksResponse {
  books: Book[];
  page: number;
  pageSize: number;
  total: number;
}

export type Ownership = "OWN" | "LOVE" | "WANT_TO_READ";

export const ownershipMap: Record<Ownership, string> = {
  OWN: "Own",
  LOVE: "Love",
  WANT_TO_READ: "Want to read",
};

export interface BooksSearchParams {
  isbn?: string;
  author?: string;
  ownership?: string;
  page?: number;
  pageSize?: number;
  sortBy?: string;
  sortOrder?: "asc" | "desc";
}
