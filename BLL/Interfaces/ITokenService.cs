using DAL.Models;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
    }
}
