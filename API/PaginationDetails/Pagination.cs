using System.Collections.Generic;

namespace API.PaginationDetails
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyCollection<T> Data { get; set; }

        public Pagination(int pageIndex, int count, int pageSize, IReadOnlyCollection<T> data)
        {
            PageIndex = pageIndex;
            Count = count;
            PageSize = pageSize;
            Data = data;
        }
    }
}
