using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Services;
using Misa.AssetManagement.Core.Services;

namespace Misa.AssetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController(IAssetService assetService) : ControllerBase
    {
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var entity = await assetService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AssetCreateDto assetCreateDto)
        {
            var createdAsset = await assetService.CreateAssetAsync(assetCreateDto);
            var response = ResponseDto<Asset>.SuccessResponse(
                data: createdAsset,
                userMessage: "Thêm mới tài sản thành công"
                );
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Asset asset)
        {
            var updatedEntity = await assetService.UpdateAsync(id, asset);
            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await assetService.DeleteAsync(id);
            return NoContent();
        }
    }
}