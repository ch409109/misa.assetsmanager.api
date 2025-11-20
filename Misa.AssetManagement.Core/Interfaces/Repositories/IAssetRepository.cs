using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface định nghĩa các phương thức truy xuất dữ liệu tài sản
    /// </summary>
    /// Created by: CongHT - 16/11/2025
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        /// <summary>
        /// Lấy danh sách tài sản với thông tin chi tiết (phòng ban, loại tài sản) có phân trang và lọc
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
        /// Lấy mã tài sản lớn nhất hiện có
        /// </summary>
        /// <returns>Mã tài sản lớn nhất</returns>
        /// Created by: CongHT - 20/11/2025
        Task<string?> GetMaxAssetCodeAsync();
    }
}
