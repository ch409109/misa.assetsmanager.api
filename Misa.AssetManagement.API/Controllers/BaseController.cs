using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.AssetManagement.Core.Interfaces.Services;

namespace Misa.AssetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T>(IBaseService<T> baseService) : ControllerBase where T : class
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await baseService.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await baseService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(T entity)
        {
            var createdEntity = await baseService.CreateAsync(entity);
            return Ok(createdEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, T entity)
        {
            var updatedEntity = await baseService.UpdateAsync(id, entity);
            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await baseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
