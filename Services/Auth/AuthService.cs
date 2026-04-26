using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Users;
using DHC_FSAP.Models.Users;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using DHC_FSAP.Helpers;
using DHC_FSAP.Services.jwt;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DHC_FSAP.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IJWTService _jwtService;

        public AuthService(AppDbContext context, EmailService emailService, IConfiguration configuration, IJWTService jWTService) 
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
            _jwtService = jWTService;
        }

        //for user register a user
        public async Task<UserResponseDTO> RegisterAsync(UserRegisterDTO dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                throw new Exception("Email already exists");
            if (_context.Users.Any(u => u.UserName == dto.UserName))
                throw new Exception("Username already exists");

            var user = new User
            {
                Name = dto.Name,
                UserName = dto.UserName,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            if (dto.ImageURL != null)
            {
                user.ImageURL = await HelperFunctionsClass.SaveImageAsync(dto.ImageURL);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = Guid.NewGuid().ToString();
            var verification = new EmailVerification
            {
                UserId = user.Id,
                Token = token
            };

            _context.EmailVerifications.Add(verification);
            await _context.SaveChangesAsync();

            //send email with this link
            var frontendURL = _configuration["FrontendURL"];
            var verifyLink = $"{frontendURL}/verify-email?token={token}";
            _emailService.SendEmail(user.Email, "Verify your email",
                $"<h3>Hello {user.Name},</h3>" +
                $"<p>Please click the link below to verify your email:</p>" +
                $"<a href='{verifyLink}'><strong>Verify Email<strong></a>" +
                $"<p><strong>This link will be expired within 12 hours.</strong></p>");

            return MapUser(user);
        }

        //verify email
        public async Task<bool> VerifyEmailAsync(string token)
        {
            var record = await _context.EmailVerifications
                .FirstOrDefaultAsync(x => x.Token == token);

            if (record == null)
                throw new Exception("record not found");

            if (record.Expire < DateTime.Now)
                throw new Exception("Time is over. Please get a new email.");

            if (record.IsUsed == true)
                throw new Exception("Your email is already verified. If not, get a new email");

            var user = await _context.Users.FindAsync(record.UserId);
            user.EmailConfirmed = true;
            _context.EmailVerifications.Remove(record);

            await _context.SaveChangesAsync();

            return true;
        }

        // for user login user
        public async Task<LoginResponseDTO> LoginAsync(UserLoginDTO dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            

            if (!user.EmailConfirmed)
                throw new Exception("Please verify your account before login");

            if (user.IsBlocked)
                throw new Exception("Your account is blocked");

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDTO
            {
                User = MapUser(user),
                Token = token
            };
        }

        //to get user profile 
        public async Task<UserResponseDTO> GetProfileAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return MapUser(user);
        }

        //to update profile
        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileDTO dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;
            if (_context.Users.Any(u => u.UserName == dto.UserName))
                throw new Exception("Username already exists");

            if (dto.Name != null) user.Name = dto.Name;
            if (dto.UserName != null) user.UserName = dto.UserName;
            if (dto.Email != null) user.Email = dto.Email;
            if (dto.Phone != null) user.Phone = dto.Phone;
            if (dto.ImageURL != null) user.ImageURL = await HelperFunctionsClass.SaveImageAsync(dto.ImageURL);

            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        //for forget password
        public async Task<bool> ForgetPasswordAsync(ForgetPasswordDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return false;

            user.ResetToken = Guid.NewGuid().ToString();
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);

            await _context.SaveChangesAsync();

            var frontendURL = _configuration["FrontendURL"];
            var link = $"{frontendURL}/reset-password?token={user.ResetToken}";

            _emailService.SendEmail(user.Email, "Reset your password",
                $"<h3>Hello {user.Name},</h3>" +
                $"<p>Please click the link below to reseet your password:</p>" +
                $"<a href='{link}'><strong>Reset Password<strong></a>" +
                $"<p><strong>This link will be expired within 30 minutes.</strong></p>");

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.ResetToken == dto.Token);

            if (user == null)
                throw new Exception("Record not found");

            if (user.ResetTokenExpiry < DateTime.UtcNow)
                throw new Exception("Time is over please request again.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResendVerificationAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new Exception("Record not found");

            if (user.EmailConfirmed)
                throw new Exception("You're already verified");

            var oldToken = _context.EmailVerifications
                .Where(x => x.UserId == user.Id);

            _context.EmailVerifications.RemoveRange(oldToken);

            var token = Guid.NewGuid().ToString();

            var verification = new EmailVerification
            {
                UserId = user.Id,
                Token = token,
            };

            _context.EmailVerifications.Add(verification);
            await _context.SaveChangesAsync();

            var frontendURL = _configuration["FrontendURL"];
            var verifyLink = $"{frontendURL}/verify-email?token={token}";

            _emailService.SendEmail(user.Email, "Verify your email",
                $"<h3>Hello {user.Name},</h3>" +
                $"<p>Please click the link below to verify your email:</p>" +
                $"<a href='{verifyLink}'><strong>Verify Email<strong></a>" +
                $"<p><strong>This link will be expired within 12 hours.</strong></p>");

            return true;
        }

        //change password
        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDTO dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            //verify old password
            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                throw new Exception("Current password is incorrect");

            // hash new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        //get all users
        public async Task<List<UserListDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserListDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    UserName = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    ImageURL = u.ImageURL,
                    Role = u.Role.ToString(),
                    IsBlocked = u.IsBlocked,
                    EmailConfirmed = u.EmailConfirmed,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }

        //get user by id
        public async Task<UserListDTO> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            return new UserListDTO
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                ImageURL = user.ImageURL,
                Role = user.Role.ToString(),
                IsBlocked = user.IsBlocked,
                EmailConfirmed = user.EmailConfirmed,
                CreatedAt = user.CreatedAt
            };
        }

        //block user
        public async Task<bool> BlockUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsBlocked = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        //unblock user
        public async Task<bool> UnblockUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsBlocked = false;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        // assign role
        public async Task<bool> UpdateUserRoleAsync(UpdateUserRoleDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null) return false;

            user.Role = dto.Role;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        //delete user
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Helper Functions
        private UserResponseDTO MapUser(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                ImageURL = user.ImageURL,
                Role = user.Role.ToString()
            };
        }

    }
}
