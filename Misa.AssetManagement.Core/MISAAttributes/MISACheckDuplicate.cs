using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.MISAAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISACheckDuplicate : Attribute
    {
        public string ErrorMessage { get; set; }
        public MISACheckDuplicate(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
