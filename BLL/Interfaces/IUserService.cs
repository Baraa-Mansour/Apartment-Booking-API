using Contracts.DTOs.user;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public Task<(List<ListItemUser> Items, int Count)> GetAllWithCountAsync(UserFilterParams filterParams);
        public Task ApproveUserAsync(int userId, int ownerId);
        public Task RejectUserAsync(int userId, int ownerId);
    }
}
