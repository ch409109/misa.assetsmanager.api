using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.MISAAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public string ErrorMessage { get; set; }
        public MISARequired(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
