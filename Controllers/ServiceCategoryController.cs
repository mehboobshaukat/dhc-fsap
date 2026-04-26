using DHC_FSAP.DTOs.Services;
using DHC_FSAP.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/service-categories")]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService _service;

        public ServiceCategoryController(IServiceCategoryService service)
        {
            _service = service;
        }

        //create
        [HttpPost("create")]
        public async Task<IActionResult> Create(ServiceCategoryCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        // get all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        //get by id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        //update
        [HttpPut("update")]
        public async Task<IActionResult> Update(ServiceCategoryUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }

        //delete
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
