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
    public class ValidateExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidateExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

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
