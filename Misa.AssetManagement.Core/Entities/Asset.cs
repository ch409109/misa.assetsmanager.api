using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Entities
{
    [Table("asset")]
    public class Asset
    {
        [Key]
        [Column("asset_id")]
        public string AssetId { get; set; }
        [Required]
        [Column("asset_code")]
        public string AssetCode { get; set; }
        [Column("asset_name")]
        public string AssetName { get; set; }
        [Column("asset_purchase_date")]
        public DateTime AssetPurchaseDate { get; set; }
        [Column("asset_usage_year")]
        public int AssetUsageYear { get; set; }
        [Column("asset_tracking_start_year")]
        public int AssetTrackingStartYear { get; set; }
        [Required]
        [Column("asset_quantity")]
        public decimal AssetQuantity { get; set; }
        [Required]
        [Column("asset_original_cost")]
        public decimal AssetOriginalCost { get; set; }
        [Required]
        [Column("asset_annual_depreciation")]
        public decimal AssetAnnualDepreciation { get; set; }
        [Required]
        [Column("department_id")]
        public Guid DepartmentId { get; set; }
        [Required]
        [Column("asset_type_id")]
        public Guid AssetTypeId { get; set; }
    }
}
