using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Repositories;
using Misa.AssetManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Services
{
    /// <summary>
    /// Service xử lý nghiệp vụ liên quan đến phòng ban
    /// </summary>
    /// Created by: CongHT - 16/11/2025
    public class DepartmentService(IBaseRepository<Department> baseRepository) : BaseService<Department>(baseRepository), IDepartmentService
    {
    }
}
