﻿namespace Core.Specifications
{
    public class ProductQueryParameters
    {
        public string Sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }

        private const int MaxPageValue = 50;

        private int _pageSize = 3;

        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value < MaxPageValue) ? value : MaxPageValue;
        }

    }
}
