using Microsoft.AspNetCore.Http;
using Misa.AssetManagement.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Exceptions
{
    /// <summary>
    /// Middleware xử lý các exception và chuyển đổi thành response chuẩn
    /// </summary>
    /// Created by: CongHT - 17/11/2025
    public class ValidateExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Khởi tạo middleware
        /// </summary>
        /// <param name="next">Request delegate tiếp theo trong pipeline</param>
        /// Created by: CongHT - 17/11/2025
        public ValidateExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Xử lý request và bắt các exception
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// Created by: CongHT - 17/11/2025
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, 400, ex.Message, "Validation Error", new List<string> { ex.Message });
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, 404, ex.Message, "Not Found Error");
            }
            catch (DuplicateException ex)
            {
                await HandleExceptionAsync(context, 409, ex.Message, "Duplicate Error", new List<string>
                {
                    $"Trường: {ex.DuplicateField}",
                    $"Giá trị đã tồn tại: {ex.ExistingName}"
                });
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, 500, "Đã xảy ra lỗi trong quá trình xử lý.", ex.Message);
            }
        }

        /// <summary>
        /// Xử lý exception và trả về response lỗi chuẩn
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="statusCode">Mã HTTP status</param>
        /// <param name="userMessage">Thông báo lỗi cho người dùng</param>
        /// <param name="systemMessage">Thông báo lỗi hệ thống</param>
        /// <param name="validateInfo">Danh sách thông tin validate chi tiết</param>
        /// Created by: CongHT - 17/11/2025
        private async Task HandleExceptionAsync(
            HttpContext context,
            int statusCode,
            string userMessage,
            string? systemMessage = null,
            List<string>? validateInfo = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = ResponseDto<object>.ErrorResponse(
                code: statusCode,
                userMessage: userMessage,
                systemMessage: systemMessage,
                validateInfo: validateInfo
            );

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var jsonResponse = JsonSerializer.Serialize(response, jsonOptions);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
