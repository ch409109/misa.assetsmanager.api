using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Exceptions
{
    public class DuplicateException : Exception
    {
        public string? DuplicateField { get; set; }
        public string? ExistingName { get; set; }

        public DuplicateException(string message) : base(message)
        {
        }

        public DuplicateException(string message, string duplicateField, string existingName) : base(message)
        {
            DuplicateField = duplicateField;
            ExistingName = existingName;
        }
    }
}
