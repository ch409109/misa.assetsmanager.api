using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Interfaces.Services
{
    /// <summary>
    /// Interface định nghĩa các phương thức nghiệp vụ liên quan đến tài sản
    /// </summary>
    /// Created by: CongHT - 16/11/2025
    public interface IAssetService : IBaseService<Asset>
    {
        /// <summary>
        /// Lấy danh sách tài sản với thông tin chi tiết có phân trang và lọc
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên mỗi trang</param>
        /// <param name="pageNumber">Số thứ tự trang</param>
        /// <param name="keyword">Từ khóa tìm kiếm (có thể null)</param>
        /// <param name="departmentName">Tên phòng ban để lọc (có thể null)</param>
        /// <param name="assetTypeName">Tên loại tài sản để lọc (có thể null)</param>
        /// <returns>Kết quả phân trang chứa danh sách tài sản và thông tin tổng hợp</returns>
        /// Created by: CongHT - 16/11/2025
        Task<PagedResult<AssetListDto>> GetAllAssetsWithDetailsAsync(
            int pageSize,
            int pageNumber,
            string? keyword,
            string? departmentName = null,
            string? assetTypeName = null);

        /// <summary>
        /// Tạo mới tài sản từ DTO
        /// </summary>
        /// <param name="assetCreateDto">DTO chứa thông tin tài sản cần tạo</param>
        /// <returns>Tài sản đã được tạo</returns>
        /// Created by: CongHT - 16/11/2025
        Task<Asset> CreateAssetAsync(AssetCreateDto assetCreateDto);

        /// <summary>
        /// Sinh mã tài sản mới tự động
        /// </summary>
        /// <returns>Mã tài sản mới</returns>
        /// Created by: CongHT - 20/11/2025
        Task<string> GenerateNewAssetCodeAsync();
    }
}
