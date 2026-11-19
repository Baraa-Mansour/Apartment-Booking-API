using Contracts.DTOs.user;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public Task<(List<ListItemUser> Items, int Count)> GetAllWithCountAsync(UserFilterParams filterParams);
    }
}
