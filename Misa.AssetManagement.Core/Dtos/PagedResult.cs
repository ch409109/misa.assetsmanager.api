using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Dtos
{
    /// <summary>
    /// DTO chứa kết quả phân trang và thông tin tổng hợp
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của items</typeparam>
    /// Created by: CongHT - 19/11/2025
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
