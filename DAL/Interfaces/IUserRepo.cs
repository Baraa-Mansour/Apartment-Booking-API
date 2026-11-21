using Contracts.DTOs.user;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserRepo
    {
        public Task<bool> ExistsByPhoneAsync(string phone);
        public Task AddUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public Task<User?> GetActiveUserByPhoneAsync(string phone);
        public Task<(List<ListItemUser> Items, int Count)> GetAllWithCountAsync(UserFilterParams filterParams);
        public Task<User?> GetUserByIdAsync(int userId);
    }
}
