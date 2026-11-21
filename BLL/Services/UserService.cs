using BLL.Exceptions;
using BLL.Interfaces;
using Contracts.DTOs.user;
using DAL.Interfaces;
using DAL.Models;
using Contracts.Enums;


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

        public async Task ApproveUserAsync(int userId, int adminId)
        {
            User? admin = await _userRepo.GetUserByIdAsync(adminId);
            if (admin is null || !IsActiveAdmin(admin))
                throw new ForbiddenException("Only Active Admin Can Approve Users");

            User? user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User Not Found");
            if (!ValidToApproved(user))
                throw new ConflictException("Only Pending User Can Be Approved");

            ReviewUserStatus(user, UserStatus.Approved, adminId);

            await _userRepo.UpdateUserAsync(user);
        }

        public async Task RejectUserAsync(int userId, int adminId)
        {
            User? admin = await _userRepo.GetUserByIdAsync(adminId);
            if (admin is null || !IsActiveAdmin(admin))
                throw new ForbiddenException("Only Active Admin Can Reject Users");

            User? user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User Not Found");
            if (!ValidToRejected(user))
                throw new ConflictException("Only Pending User Can Be Rejected");

            ReviewUserStatus(user, UserStatus.Rejected, adminId);

            await _userRepo.UpdateUserAsync(user);
        }

        static private bool IsActiveAdmin(User? admin)
        {
            return (admin != null && admin.Role == UserRoles.Admin && admin.Status == UserStatus.Approved);
        }
        static private bool ValidToApproved(User? user)
        {
            return (user is not null && user.Status == UserStatus.Pending && user.DeletedAt is null);  
        }
        static private bool ValidToRejected(User? user)
        {
            return (user is not null && user.Status == UserStatus.Pending && user.DeletedAt is null);
        }
        static private void ReviewUserStatus(User user, UserStatus status, int adminId)
        {
            DateTime currentDateTime = DateTime.UtcNow;

            user.Status = status;
            user.ReviewedBy = adminId;
            user.ReviewedAt = currentDateTime;
            user.UpdatedAt = currentDateTime;
        }
    }
}
