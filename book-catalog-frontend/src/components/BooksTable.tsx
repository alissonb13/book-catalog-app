import {
  DataGrid,
  type GridColDef,
  type GridSortModel,
  type GridPaginationModel,
} from "@mui/x-data-grid";
import { Box, CircularProgress, Alert } from "@mui/material";
import { useBooks } from "../context/BooksContext";
import { useEffect, useState } from "react";
import { ownershipMap, type Ownership } from "../types/book";

export default function BooksTable() {
  const { response, loading, search } = useBooks();
  const books = response?.books ?? [];

  const [paginationModel, setPaginationModel] = useState({
    page: 0,
    pageSize: 10,
  });

  const columns: GridColDef[] = [
    { field: "title", headerName: "Title", flex: 1, sortable: true },
    { field: "author", headerName: "Author", flex: 1, sortable: true },
    { field: "isbn", headerName: "ISBN", flex: 1 },
    {
      field: "ownership",
      headerName: "Ownership",
      flex: 1,
      valueFormatter: (param) => {
        return ownershipMap[String(param).toUpperCase() as Ownership] ?? param;
      },
    },
  ];

  const handleSortModelChange = (sortModel: GridSortModel) => {
    if (sortModel.length > 0) {
      const { field, sort } = sortModel[0];
      search({
        sortBy: field as keyof (typeof books)[0],
        sortOrder: sort as "asc" | "desc",
        page: paginationModel.page,
        pageSize: paginationModel.pageSize,
      });
    }
  };

  const handlePaginationModelChange = (model: GridPaginationModel) => {
    setPaginationModel(model);
    search({
      page: model.page + 1,
      pageSize: model.pageSize,
    });
  };

  useEffect(() => {
    search({
      page: paginationModel.page + 1,
      pageSize: paginationModel.pageSize,
    });
  }, []);

  if (loading) return <CircularProgress sx={{ m: 2 }} />;
  if (!books.length) return <Alert severity="info">No books found</Alert>;

  return (
    <Box
      sx={{
        width: "100%",
      }}
    >
      {books.length ? (
        <DataGrid
          rows={books}
          columns={columns}
          getRowId={(row) => row.isbn}
          sortingMode="server"
          onSortModelChange={handleSortModelChange}
          paginationMode="server"
          rowCount={response?.total}
          pageSizeOptions={[5, 10, 20, 100]}
          paginationModel={paginationModel}
          onPaginationModelChange={handlePaginationModelChange}
        />
      ) : (
        <Alert severity="info">No books found</Alert>
      )}
    </Box>
  );
}
