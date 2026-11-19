using Contracts.DTOs.user;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserRepo
    {
        public Task<bool> ExistsByPhoneAsync(string phone);
        public Task AddUserAsync(User user);
        public Task<User?> FindActiveUserByPhoneAsync(string phone);
        public Task<(List<ListItemUser> Items, int Count)> GetAllWithCountAsync(UserFilterParams filterParams);
    }
}
