using DAL.Models;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
        public Task AddRefreshTokenAsync(string refreshToken, int userId);
    }
}
