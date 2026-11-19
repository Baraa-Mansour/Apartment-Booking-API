using BLL.Interfaces;
using Contracts.DTOs.user;
using DAL.Interfaces;


namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<(List<ListItemUser> Items, int Count)> GetAllWithCountAsync(UserFilterParams filterParams)
        {
            return await _userRepo.GetAllWithCountAsync(filterParams);
        }
    }
}
