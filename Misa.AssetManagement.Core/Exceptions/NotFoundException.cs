using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Exceptions
{
    /// <summary>
    /// Exception được throw khi không tìm thấy tài nguyên
    /// </summary>
    /// Created by: CongHT - 17/11/2025
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Khởi tạo exception với thông báo lỗi
        /// </summary>
        /// <param name="message">Thông báo lỗi</param>
        /// Created by: CongHT - 17/11/2025
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo exception với thông báo lỗi và exception gốc
        /// </summary>
        /// <param name="message">Thông báo lỗi</param>
        /// <param name="innerException">Exception gốc</param>
        /// Created by: CongHT - 17/11/2025
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
