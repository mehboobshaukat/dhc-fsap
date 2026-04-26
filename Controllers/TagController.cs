using DHC_FSAP.DTOs.Tags;
using DHC_FSAP.Services.Tags;
using Microsoft.AspNetCore.Mvc;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        //get all
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
            if (result == null) return NotFound();
            return Ok(result);
        }

        //create
        [HttpPost("create-tag")]
        public async Task<IActionResult> Create(TagCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        //update
        [HttpPut("update-tag")]
        public async Task<IActionResult> Update(TagUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if(!result) return NotFound();
            return Ok(result);
        }

        //delete
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
