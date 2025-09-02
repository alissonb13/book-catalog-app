import type { BooksResponse, BooksSearchParams } from "../types/book";

const API_URL = import.meta.env.VITE_API_URL ?? "http://localhost:5296";

export async function fetchBooks(
  params: BooksSearchParams
): Promise<BooksResponse> {
  const query = new URLSearchParams(params as any).toString();
  const res = await fetch(`${API_URL}/books?${query}`);

  if (!res.ok) {
    console.log(await res.json());
    throw new Error("Error while fetching books from API");
  }

  return await res.json();
}
