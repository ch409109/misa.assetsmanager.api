using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Services;

namespace Misa.AssetManagement.API.Controllers
{
    /// <summary>
    /// Controller xử lý các API liên quan đến phòng ban
    /// </summary>
    /// Created by: CongHT - 19/11/2025
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IDepartmentService departmentService) : BaseController<Department>(departmentService)
    {
    }
}
