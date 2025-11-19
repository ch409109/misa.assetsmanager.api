using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Services;
using Misa.AssetManagement.Core.Services;

namespace Misa.AssetManagement.API.Controllers
{
    /// <summary>
    /// Controller xử lý các API liên quan đến tài sản
    /// </summary>
    /// Created by: CongHT - 16/11/2025
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController(IAssetService assetService) : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách tài sản có phân trang và lọc
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên mỗi trang (mặc định 12)</param>
        /// <param name="pageNumber">Số thứ tự trang (mặc định 1)</param>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="departmentName">Tên phòng ban để lọc</param>
        /// <param name="assetTypeName">Tên loại tài sản để lọc</param>
        /// <returns>Danh sách tài sản có phân trang</returns>
        /// Created by: CongHT - 16/112025
        [HttpGet]
        public new async Task<IActionResult> GetAll(
            [FromQuery] int pageSize = 12,
            [FromQuery] int pageNumber = 1,
            [FromQuery] string? keyword = null,
            [FromQuery] string? departmentName = null,
            [FromQuery] string? assetTypeName = null)
        {

            var pagedResult = await assetService.GetAllAssetsWithDetailsAsync(
                pageSize, pageNumber, keyword, departmentName, assetTypeName);

            var response = ResponseDto<PagedResult<AssetListDto>>.SuccessResponse(
                data: pagedResult,
                userMessage: "Lấy danh sách tài sản thành công"
                );
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin tài sản theo ID
        /// </summary>
        /// <param name="id">ID của tài sản</param>
        /// <returns>Thông tin tài sản. Luôn trả về 200 OK với trường Success để phân biệt kết quả</returns>
        /// Created by: CongHT - 16/112025
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var entity = await assetService.GetByIdAsync(id);
            var response = new ResponseDto<Asset>
            {
                Success = entity != null,
                Data = entity,
                UserMessage = entity != null
                ? "Lấy thông tin tài sản thành công"
                : "Không tìm thấy tài sản"
            };
            return Ok(response);
        }

        /// <summary>
        /// Tạo mới tài sản
        /// </summary>
        /// <param name="assetCreateDto">Thông tin tài sản cần tạo</param>
        /// <returns>Tài sản đã được tạo</returns>
        /// Created by: CongHT - 16/112025
        [HttpPost("new")]
        public async Task<IActionResult> Create([FromBody] AssetCreateDto assetCreateDto)
        {
            var createdAsset = await assetService.CreateAssetAsync(assetCreateDto);
            var response = ResponseDto<Asset>.SuccessResponse(
                data: createdAsset,
                userMessage: "Thêm mới tài sản thành công"
                );
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật thông tin tài sản
        /// </summary>
        /// <param name="id">ID của tài sản cần cập nhật</param>
        /// <param name="asset">Thông tin tài sản mới</param>
        /// <returns>Kết quả cập nhật</returns>
        /// Created by: CongHT - 16/112025
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Asset asset)
        {
            var updatedEntity = await assetService.UpdateAsync(id, asset);
            return Ok(updatedEntity);
        }

        /// <summary>
        /// Xóa tài sản theo ID
        /// </summary>
        /// <param name="id">ID của tài sản cần xóa</param>
        /// <returns>Kết quả xóa</returns>
        /// Created by: CongHT - 16/112025
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await assetService.DeleteAsync(id);
            return NoContent();
        }
    }
}