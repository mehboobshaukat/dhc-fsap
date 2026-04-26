using DHC_FSAP.DTOs.Abouts;
using DHC_FSAP.Services.About;
using Microsoft.AspNetCore.Mvc;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/team")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _tamService;

        public TeamController(ITeamService tamService)
        {
            _tamService = tamService;
        }

        //public side
        [HttpGet("team-getby-public")]
        public async Task<IActionResult> GetTeamByPublic()
        {
            var result = await _tamService.GetTeamByPublicAsync();
            return Ok(result);
        }

        //admin side
        [HttpGet("team-getby-admin")]
        public async Task<IActionResult> GetTeamByAdmin()
        {
            var result = await _tamService.GetTeamByAdminAsync();

            return Ok(result);
        }

        //get by id
        [HttpGet("get-team-by-it/{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var result = await _tamService.GetTeamByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        //create team
        [HttpPost("create-team")]
        public async Task<IActionResult> CreateTeam(TeamCreateDTO dto)
        {
            var result = await _tamService.CreateTeamAsync(dto);
            return Ok(result);
        }

        //update team
        [HttpPut("update-team")]
        public async Task<IActionResult> UpdateTeam(TeamUpdateDTO dto)
        {
            var result = await _tamService.UpdateTeamAsync(dto);

            if (!result) return NotFound();
            return Ok(result);
        }

        //delete team
        [HttpDelete("delete-team/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var result = await _tamService.DeleteTeamAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
