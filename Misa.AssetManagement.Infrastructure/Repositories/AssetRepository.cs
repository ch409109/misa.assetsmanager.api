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
                whereClauses.Add("(a.asset_code LIKE @Keyword OR a.asset_name LIKE @Keyword OR c.category_name LIKE @Keyword)");
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
                ORDER BY a.asset_code
                LIMIT @PageSize OFFSET @Offset";

            var countSql = $@"
                SELECT COUNT(DISTINCT asset_id)
                FROM asset a
                INNER JOIN asset_type at ON a.asset_type_id = at.asset_type_id
                INNER JOIN department d ON a.department_id = d.department_id
                {whereClause}";

            var sql = $"{sqlCommand}; {countSql};";

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var multi = await connection.QueryMultipleAsync(sql, parameters))
                {
                    var data = await multi.ReadAsync<AssetListDto>();
                    var totalCount = await multi.ReadSingleAsync<int>();

                    return new PagedResult<AssetListDto>
                    {
                        Data = data,
                        TotalCount = totalCount
                    };
                }
            }
        }
    }
}
