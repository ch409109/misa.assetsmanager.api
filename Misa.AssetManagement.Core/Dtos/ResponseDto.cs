using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Dtos
{
    /// <summary>
    /// DTO chuẩn cho response API
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
    /// Created by: CongHT - 17/11/2025
    public class ResponseDto<T>
    {
        public bool Success { get; set; }

        public int Code { get; set; }

        public int SubCode { get; set; }

        public string? UserMessage { get; set; }

        public string? SystemMessage { get; set; }

        public T? Data { get; set; }

        public List<string> ValidateInfo { get; set; } = new List<string>();

        public DateTime ServerTime { get; set; }

        public bool GetLastData { get; set; }

        /// <summary>
        /// Tạo response thành công (HTTP 200)
        /// </summary>
        /// <param name="data">Dữ liệu trả về</param>
        /// <param name="userMessage">Thông báo cho người dùng</param>
        /// <returns>ResponseDto với Success = true, Code = 200</returns>
        /// Created by: CongHT - 19/11/2025
        public static ResponseDto<T> SuccessResponse(T data, string? userMessage = null)
        {
            return new ResponseDto<T>
            {
                Success = true,
                Code = 200,
                SubCode = 0,
                UserMessage = userMessage,
                SystemMessage = null,
                Data = data,
                ValidateInfo = new List<string>(),
                ServerTime = DateTime.Now,
                GetLastData = true
            };
        }

        /// <summary>
        /// Tạo response lỗi (HTTP 4xx, 5xx)
        /// </summary>
        /// <param name="code">Mã lỗi HTTP (400, 404, 500...)</param>
        /// <param name="userMessage">Thông báo lỗi cho người dùng</param>
        /// <param name="systemMessage">Thông báo hệ thống (cho developer)</param>
        /// <param name="validateInfo">Danh sách lỗi validation chi tiết</param>
        /// <returns>ResponseDto với Success = false</returns>
        /// Created by: CongHT - 19/11/2025
        public static ResponseDto<T> ErrorResponse(
            int code,
            string userMessage,
            string? systemMessage = null,
            List<string>? validateInfo = null)
        {
            return new ResponseDto<T>
            {
                Success = false,
                Code = code,
                SubCode = 0,
                UserMessage = userMessage,
                SystemMessage = systemMessage,
                Data = default(T),
                ValidateInfo = validateInfo ?? new List<string>(),
                ServerTime = DateTime.Now,
                GetLastData = false
            };
        }
    }
}
