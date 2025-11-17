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
    [MISATableName("department")]
    public class Department
    {
        [MISAKey]
        [MISAColumnName("department_id")]
        public string DepartmentId { get; set; }
        [MISAColumnName("department_name")]
        public string DepartmentName { get; set; }
        [MISARequired("Không được để trống")]
        [MISAColumnName("department_code")]
        public string DepartmentCode { get; set; }
    }
}
