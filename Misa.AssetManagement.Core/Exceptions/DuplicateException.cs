using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Exceptions
{
    /// <summary>
    /// Exception được throw khi phát hiện dữ liệu trùng lặp
    /// </summary>
    /// Created by: CongHT - 19/11/2025
    public class DuplicateException : Exception
    {
        public string? DuplicateField { get; set; }

        public string? ExistingName { get; set; }

        /// <summary>
        /// Khởi tạo exception với thông báo lỗi
        /// </summary>
        /// <param name="message">Thông báo lỗi</param>
        /// Created by: CongHT - 19/11/2025
        public DuplicateException(string message) : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo exception với thông báo lỗi và thông tin trường bị trùng
        /// </summary>
        /// <param name="message">Thông báo lỗi</param>
        /// <param name="duplicateField">Tên trường bị trùng lặp</param>
        /// <param name="existingName">Giá trị đã tồn tại</param>
        /// Created by: CongHT - 19/11/2025
        public DuplicateException(string message, string duplicateField, string existingName) : base(message)
        {
            DuplicateField = duplicateField;
            ExistingName = existingName;
        }
    }
}
