using Microsoft.Extensions.Configuration;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository xử lý nghiệp vụ liên quan đến phòng ban
    /// </summary>
    /// Created by: CongHT - 19/11/2025
    public class DepartmentRepository(IConfiguration configuration) : BaseRepository<Department>(configuration), IDepartmentRepository
    {
    }
}
