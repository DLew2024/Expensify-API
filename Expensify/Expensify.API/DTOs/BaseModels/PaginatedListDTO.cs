namespace Expensify.API.DTOs.BaseModels;
    public class PaginatedListDTO<T>
    {
        public int CurrentPage { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<T> Items { get; set; }

        public PaginatedListDTO(List<T> items, int totalPages, int pageNumber, int pageSize, int totalRecords)
        {
            CurrentPage = pageNumber;
            RecordsPerPage = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            Items = items;
        }
    }
