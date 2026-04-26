using DHC_FSAP.DTOs.Portfolios;
using DHC_FSAP.Services.Portfolios;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/portfolio-categories")]
public class PortfolioCategoryController : ControllerBase
{
    private readonly IPortfolioCategoryService _service;

    public PortfolioCategoryController(IPortfolioCategoryService service)
    {
        _service = service;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(PortfolioCategoryCreateDTO dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(PortfolioCategoryUpdateDTO dto)
    {
        var result = await _service.UpdateAsync(dto);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return Ok(result);
    }
}