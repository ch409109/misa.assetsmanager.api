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
    [MISATableName("asset_type")]
    public class AssetType
    {
        [MISAKey]
        [MISAColumnName("asset_type_id")]
        public Guid AssetTypeId { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("asset_type_code")]
        public string AssetTypeCode { get; set; }
        [MISAColumnName("asset_type_name")]
        public string AssetTypeName { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("depreciation_rate")]
        public decimal DepreciationRate { get; set; }
        [MISAColumnName("depreciation_year")]
        public int DepreciationYear { get; set; }
    }
}
