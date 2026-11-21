using BLL.Exceptions;
using BLL.Interfaces;
using Contracts.Configs;
using Contracts.DTOs.auth;
using Contracts.Enums;
using DAL.Interfaces;
using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserRepo userRepo, ITokenService tokenService, JwtSettings jwtSettings,
            IRefreshTokenRepo refreshTokenRepo)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
            _refreshTokenRepo = refreshTokenRepo;
        }


        public async Task RegisterAsync(RegisterRequest request)
        {
            await ValidateRegisterRequestAsync(request);

            User user = CreateUser(request);

            await _userRepo.AddUserAsync(user);
        }
        private async Task ValidateRegisterRequestAsync(RegisterRequest request)
        {
            if (request.Role is null || request.BirthDate is null)
                throw new ValidationException("Role and birth date are required.");

            if (request.Role == UserRoles.Admin)
                throw new ForbiddenException("Only Tenant or Owner can self-register.");

            if (request.BirthDate > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ValidationException("Birth date cannot be in the future.");

            var minAllowed = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18));
            if (request.BirthDate > minAllowed)
                throw new ValidationException("You must be at least 18 years old.");

            if (await _userRepo.ExistsByPhoneAsync(request.Phone))
                throw new ConflictException("Phone number already used");
        }
        private User CreateUser(RegisterRequest request)
        {
            DateTime currentDateTime = DateTime.UtcNow;

            User user = new User()
            {
                Phone = request.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role.Value,
                Status = UserStatus.Pending,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate.Value,
                ProfilePhotoPath = request.ProfilePhotoPath,
                IdPhotoPath = request.IdPhotoPath,
                Language = request.Language ?? UserLanguage.Ar,
                Theme = request.Theme ?? UserThemes.Light,
                CreatedAt = currentDateTime,
                UpdatedAt = currentDateTime,
            };

            return user;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            User? user = await _userRepo.GetActiveUserByPhoneAsync(request.Phone);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid credentials");

            LoginResponse response = new LoginResponse()
            {
                AccessToken = _tokenService.GenerateAccessToken(user),
                RefreshToken = _tokenService.GenerateRefreshToken()
            };

            RefreshToken refreshToken = CreateRefreshToken(response.RefreshToken, user.UserId);
            await _refreshTokenRepo.AddRefreshTokenAsync(refreshToken);

            return response;
        }
        private RefreshToken CreateRefreshToken(string refreshToken,  int userId)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            RefreshToken token = new RefreshToken()
            {
                UserId = userId,
                TokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken),
                CreatedAt = currentDateTime,
                ExpiresAt = currentDateTime.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };

            return token;
        }
    }
}
