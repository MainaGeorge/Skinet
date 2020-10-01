﻿using System.Collections.Generic;

namespace API.PaginationDetails
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyCollection<T> Data { get; set; }  
    }
}
