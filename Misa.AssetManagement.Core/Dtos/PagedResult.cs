using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Dtos
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public int TotalCount { get; set; } = 0;
        public decimal TotalQuantity { get; set; } = 0;
        public decimal TotalOriginalCost { get; set; } = 0;
        public decimal TotalAccumulatedDepreciation { get; set; } = 0;
        public decimal TotalRemainingValue { get; set; } = 0;
    }
}
