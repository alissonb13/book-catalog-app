import { useState } from "react";
import {
  TextField,
  Button,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
  Box,
} from "@mui/material";
import { useBooks } from "../context/BooksContext";
import type { Ownership } from "../types/book";

export default function SearchBooksForm() {
  const { search } = useBooks();
  const [isbn, setIsbn] = useState<string>("");
  const [author, setAuthor] = useState<string>("");
  const [ownership, setOwnership] = useState<Ownership | "">("");

  function handleSubmit(event: React.FormEvent) {
    event.preventDefault();
    search({ isbn, author, ownership, page: 1 });
  }

  const handleReset = () => {
    setIsbn("");
    setAuthor("");
    setOwnership("");
    search({});
  };

  return (
    <Box component="form" display="flex" gap={2} mb={2} onSubmit={handleSubmit}>
      <TextField
        label="Author"
        value={author}
        onChange={(e) => setAuthor(e.target.value)}
      />
      <TextField
        label="ISBN"
        value={isbn}
        onChange={(e) => setIsbn(e.target.value)}
      />
      <FormControl sx={{ minWidth: 160 }}>
        <InputLabel>Ownership</InputLabel>
        <Select
          value={ownership}
          label="Ownership"
          onChange={(e) => setOwnership(e.target.value as Ownership)}
        >
          <MenuItem value="OWN">Own</MenuItem>
          <MenuItem value="LOVE">Love</MenuItem>
          <MenuItem value="WANT_TO_READ">Want to Read</MenuItem>
        </Select>
      </FormControl>
      <Button type="submit" variant="contained">
        Search
      </Button>
      <Button type="button" variant="outlined" onClick={handleReset}>
        Reset
      </Button>
    </Box>
  );
}
