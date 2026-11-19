using Contracts.DTOs.auth;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        public Task RegisterAsync(RegisterRequest request);
        public Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
