import { createContext, useContext, useState, type ReactNode } from "react";
import type { BooksResponse, BooksSearchParams } from "../types/book";
import { fetchBooks } from "../api/books";

interface BooksContextType {
  loading: boolean;
  response: BooksResponse | null;
  search: (params: BooksSearchParams) => void;
}

const BooksContext = createContext<BooksContextType | null>(null);

export function BooksProvider({ children }: { children: ReactNode }) {
  const [response, setResponse] = useState<BooksResponse | null>(null);
  const [loading, setLoading] = useState(false);

  async function search(params: BooksSearchParams) {
    setLoading(true);
    try {
      const data = await fetchBooks(params);
      console.log(data);
      setResponse(data);
    } finally {
      setLoading(false);
    }
  }

  return (
    <BooksContext.Provider value={{ response, loading, search }}>
      {children}
    </BooksContext.Provider>
  );
}

export function useBooks(): BooksContextType {
  const context = useContext(BooksContext);

  if (!context) {
    throw new Error("Hook useBooks need to be used inside BooksProvider");
  }

  return context;
}
