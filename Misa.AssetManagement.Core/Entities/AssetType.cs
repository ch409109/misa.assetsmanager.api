using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Entities
{
    [Table("asset_type")]
    public class AssetType
    {
        [Key]
        [Column("asset_type_id")]
        public string AssetTypeId { get; set; }
        [Required]
        [Column("asset_type_code")]
        public string AssetTypeCode { get; set; }
        [Column("asset_type_name")]
        public string AssetTypeName { get; set; }
        [Required]
        [Column("depreciation_rate")]
        public decimal DepreciationRate { get; set; }
        [Column("depreciation_year")]
        public int DepreciationYear { get; set; }
    }
}
