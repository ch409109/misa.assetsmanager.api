using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Infrastructure.Repositories
{
    public class AssetRepository(IConfiguration configuration) : BaseRepository<Asset>(configuration), IAssetRepository
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
        public async Task<PagedResult<AssetListDto>> GetAllAssetsWithDetailsAsync(
            int pageSize,
            int pageNumber,
            string? keyword,
            string? departmentName = null,
            string? assetTypeName = null)
        {
            var offset = (pageNumber - 1) * pageSize;

            var parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@Offset", offset);

            var whereClauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                whereClauses.Add("(a.asset_code LIKE @Keyword OR a.asset_name LIKE @Keyword)");
                parameters.Add("@Keyword", $"%{keyword}%");
            }

            if (!string.IsNullOrWhiteSpace(departmentName))
            {
                whereClauses.Add("d.department_name = @DepartmentName");
                parameters.Add("@DepartmentName", departmentName);
            }

            if (!string.IsNullOrWhiteSpace(assetTypeName))
            {
                whereClauses.Add("at.asset_type_name = @AssetTypeName");
                parameters.Add("@AssetTypeName", assetTypeName);
            }

            var whereClause = whereClauses.Count > 0 ? " WHERE " + string.Join(" AND ", whereClauses) : "";

            var sqlCommand = $@"
                SELECT 
                    a.asset_id AS AssetId,
                    a.asset_code AS AssetCode,
                    a.asset_name AS AssetName,
                    at.asset_type_name AS AssetTypeName,
                    d.department_name AS DepartmentName,
                    a.asset_quantity AS Quantity,
                    a.asset_original_cost AS OriginalCost,
                    (a.asset_original_cost * at.depreciation_rate / 100) AS AccumulatedDepreciation,
                    (a.asset_original_cost - (a.asset_original_cost * at.depreciation_rate / 100)) AS RemainingValue
                FROM asset a
                INNER JOIN asset_type at ON a.asset_type_id = at.asset_type_id
                INNER JOIN department d ON a.department_id = d.department_id
                {whereClause}
                ORDER BY a.asset_code DESC
                LIMIT @PageSize OFFSET @Offset";

            var countSql = $@"
                SELECT COUNT(DISTINCT asset_id)
                FROM asset a
                INNER JOIN asset_type at ON a.asset_type_id = at.asset_type_id
                INNER JOIN department d ON a.department_id = d.department_id
                {whereClause}";

            var sumSql = $@"
                SELECT 
                    COALESCE(SUM(a.asset_quantity), 0) AS TotalQuantity,
                    COALESCE(SUM(a.asset_original_cost), 0) AS TotalOriginalCost,
                    COALESCE(SUM(a.asset_original_cost * at.depreciation_rate / 100), 0) AS TotalAccumulatedDepreciation,
                    COALESCE(SUM(a.asset_original_cost - (a.asset_original_cost * at.depreciation_rate / 100)), 0) AS TotalRemainingValue
                FROM asset a
                INNER JOIN asset_type at ON a.asset_type_id = at.asset_type_id
                INNER JOIN department d ON a.department_id = d.department_id
                {whereClause}";

            var sql = $"{sqlCommand}; {countSql}; {sumSql}";

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var multi = await connection.QueryMultipleAsync(sql, parameters))
                {
                    var data = await multi.ReadAsync<AssetListDto>();
                    var totalCount = await multi.ReadSingleAsync<int>();
                    var totals = await multi.ReadSingleAsync<dynamic>();

                    return new PagedResult<AssetListDto>
                    {
                        Data = data,
                        TotalCount = totalCount,
                        TotalQuantity = totals.TotalQuantity,
                        TotalOriginalCost = totals.TotalOriginalCost,
                        TotalAccumulatedDepreciation = totals.TotalAccumulatedDepreciation,
                        TotalRemainingValue = totals.TotalRemainingValue
                    };
                }
            }
        }

        public async Task<string?> GetMaxAssetCodeAsync()
        {
            var sqlCommand = "SELECT asset_code FROM asset ORDER BY asset_code DESC LIMIT 1";

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var maxAssetCode = await connection.QueryFirstOrDefaultAsync<string?>(sqlCommand);
                return maxAssetCode;
            }
        }
    }
}
