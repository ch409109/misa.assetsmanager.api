using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Services;

namespace Misa.AssetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController(IAssetService service) : BaseController<Asset>(service)
    {
        private readonly IAssetService _assetService = service;

        [HttpGet("details")]
        public new async Task<IActionResult> GetAll(
            [FromQuery] int pageSize = 12,
            [FromQuery] int pageNumber = 1,
            [FromQuery] string? keyword = null,
            [FromQuery] string? departmentName = null,
            [FromQuery] string? assetTypeName = null)
        {

            var pagedResult = await _assetService.GetAllAssetsWithDetailsAsync(
                pageSize, pageNumber, keyword, departmentName, assetTypeName);

            var response = ResponseDto<PagedResult<AssetListDto>>.SuccessResponse(
                data: pagedResult,
                userMessage: "Lấy danh sách tài sản thành công"
                );
            return Ok(response);
        }
    }
}
