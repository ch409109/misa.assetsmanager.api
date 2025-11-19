using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.MISAAttributes
{
    /// <summary>
    /// Attribute chỉ định tên cột tương ứng trong database
    /// </summary>
    /// Created by: CongHT - 17/11/2025
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAColumnName : Attribute
    {
        public string ColumnName { get; set; }
        public MISAColumnName(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
