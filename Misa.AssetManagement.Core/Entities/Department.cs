using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Entities
{
    [Table("department")]
    public class Department
    {
        [Key]
        [Column("department_id")]
        public string DepartmentId { get; set; }
        [Column("department_name")]
        public string DepartmentName { get; set; }
        [Required]
        [Column("department_code")]
        public string DepartmentCode { get; set; }
    }
}
