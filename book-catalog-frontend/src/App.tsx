import { Container } from "@mui/material";
import { BooksProvider } from "./context/BooksContext";
import BooksTable from "./components/BooksTable";
import SearchBooksForm from "./components/SearchBooksForm";

export default function App() {
  return (
    <BooksProvider>
      <Container sx={{ mt: 4 }}>
        <SearchBooksForm />
        <BooksTable />
      </Container>
    </BooksProvider>
  );
}
