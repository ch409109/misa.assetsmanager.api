using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.MISAAttributes
{
    /// <summary>
    /// Attribute đánh dấu property là khóa chính (Primary Key)
    /// </summary>
    /// Created by: CongHT - 17/11/2025
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAKey : Attribute
    {
    }
}
