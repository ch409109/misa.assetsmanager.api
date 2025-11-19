using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Exceptions
{
    /// <summary>
    /// Exception được throw khi dữ liệu không hợp lệ (validation failed)
    /// </summary>
    /// Created by: CongHT - 17/11/2025
    public class ValidationException : Exception
    {
        /// <summary>
        /// Khởi tạo exception với thông báo lỗi
        /// </summary>
        /// <param name="message">Thông báo lỗi validation</param>
        /// Created by: CongHT - 17/11/2025
        public ValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo exception với thông báo lỗi và exception gốc
        /// </summary>
        /// <param name="message">Thông báo lỗi validation</param>
        /// <param name="innerException">Exception gốc</param>
        /// Created by: CongHT - 17/11/2025
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
