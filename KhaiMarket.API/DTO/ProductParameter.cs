using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhaiMarket.API.DTO
{
    public class ProductParameter
    {
        const int maxPageSize = 5;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 3;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public double MinRating { get; set; }

    }
}