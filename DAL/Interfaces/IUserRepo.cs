using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserRepo
    {
        public Task<bool> ExistsByPhoneAsync(string phone);
        public Task AddUserAsync(User user);
    }
}
