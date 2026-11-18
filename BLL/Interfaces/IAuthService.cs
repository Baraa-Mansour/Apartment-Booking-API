using Contracts.DTOs;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        public Task RegisterAsync(RegisterRequest request);
    }
}
