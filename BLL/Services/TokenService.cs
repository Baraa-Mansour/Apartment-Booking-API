using BLL.Interfaces;
using Contracts.Configs;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly JwtSettings _jwtSettings;

        public TokenService(IRefreshTokenRepo refreshTokenRepo, JwtSettings jwtSettings)
        {
            _refreshTokenRepo = refreshTokenRepo;
            _jwtSettings = jwtSettings;
        }


        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public async Task AddRefreshTokenAsync(string refreshToken, int userId)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            RefreshToken token = new RefreshToken()
            {
                UserId = userId,
                TokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken),
                CreatedAt = currentDateTime,
                ExpiresAt = currentDateTime.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };

            await _refreshTokenRepo.AddRefreshToken(token);
        }
    }
}
