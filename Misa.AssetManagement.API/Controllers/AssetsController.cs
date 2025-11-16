using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Services;

namespace Misa.AssetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController(IAssetService service) : BaseController<Asset>(service)
    {
    }
}
