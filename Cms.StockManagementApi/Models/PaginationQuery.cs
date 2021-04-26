
namespace Cms.StockManagementApi.Models
{
    public class PaginationQuery
    {
        public PaginationQuery() { }
        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1000;
    }
}
