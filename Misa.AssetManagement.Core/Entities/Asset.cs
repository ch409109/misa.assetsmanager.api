using Misa.AssetManagement.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Entities
{
    [MISATableName("asset")]
    public class Asset
    {
        [MISAKey]
        [MISAColumnName("asset_id")]
        public string? AssetId { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("asset_code")]
        [MISACheckDuplicate("Mã tài sản không được phép trùng")]
        public string AssetCode { get; set; }
        [MISAColumnName("asset_name")]
        public string AssetName { get; set; }
        [MISAColumnName("asset_purchase_date")]
        public DateTime AssetPurchaseDate { get; set; }
        [MISAColumnName("asset_usage_year")]
        public int AssetUsageYear { get; set; }
        [MISAColumnName("asset_tracking_start_year")]
        public int AssetTrackingStartYear { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("asset_quantity")]
        public decimal AssetQuantity { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("asset_original_cost")]
        public decimal AssetOriginalCost { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("asset_annual_depreciation")]
        public decimal AssetAnnualDepreciation { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("department_id")]
        public string DepartmentId { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("asset_type_id")]
        public string AssetTypeId { get; set; }
    }
}
