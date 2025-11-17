using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.MISAAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MISATableName : Attribute
    {
        public string TableName { get; set; }
        public MISATableName(string tableName)
        {
            TableName = tableName;
        }
    }
}
