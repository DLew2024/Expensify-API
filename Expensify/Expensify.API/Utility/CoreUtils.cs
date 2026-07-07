using Expensify.API.DTOs.BaseModels;

namespace Expensify.API.Utility
{
    public class CoreUtils
    {
        public static PaginatedListDTO<T> CreatePaginatedList<T>(
        List<T> source,
        int pageNumber,
        int pageSize,
        int totalRecords
    )
        {
            var count = source.Count;
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedListDTO<T>(items, count, pageNumber, pageSize, totalRecords);
        }
    }
}
