using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Dtos
{
    /// <summary>
    /// DTO chứa thông tin tài sản hiển thị trong danh sách
    /// </summary>
    /// Created by: CongHT - 18/11/2025
    public class AssetListDto
    {
        public string AssetId { get; set; }

        public string AssetCode { get; set; }

        public string AssetName { get; set; }

        public string AssetTypeName { get; set; }

        public string DepartmentName { get; set; }

        public decimal Quantity { get; set; }

        public decimal OriginalCost { get; set; }

        public decimal AccumulatedDepreciation { get; set; }

        public decimal RemainingValue { get; set; }
    }
}
