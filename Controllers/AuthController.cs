using DHC_FSAP.DTOs.Users;
using DHC_FSAP.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]UserRegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            try
            {
                var result = await _authService.VerifyEmailAsync(token);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _authService.GetProfileAsync(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDTO dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _authService.UpdateProfileAsync(userId, dto);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgetPasswordDTO dto)
        {
            var result = await _authService.ForgetPasswordAsync(dto);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            try
            {
                var result = await _authService.ResetPasswordAsync(dto);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _authService.ChangePasswordAsync(userId,dto);

            if (!result) return BadRequest("Password change failed");

            return Ok("Password updated successfully");
        }

        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification([FromBody] string email)
        {
            try
            {
                var result = await _authService.ResendVerificationAsync(email);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        //get all users
        [Authorize(Roles ="Admin, SuperAdmin")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        //get user by id
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _authService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        //block user
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var result = await _authService.BlockUserAsync(id);
            if(!result) return NotFound();
            return Ok(result);
        }

        //unblock user
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("unblock/{id}")]
        public async Task<IActionResult> UnBlockUser(int id)
        {
            var result = await _authService.UnblockUserAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }

        //assign role
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("assign-role")]
        public async Task<IActionResult> UpdateRole(UpdateUserRoleDTO dto)
        {
            var result = await _authService.UpdateUserRoleAsync(dto);
            if (!result) return NotFound();
            return Ok(result);
        }

        //delete
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _authService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
