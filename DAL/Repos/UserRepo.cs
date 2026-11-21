using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Contracts.Enums;
using Contracts.DTOs.user;


namespace DAL.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly ApartmentBookingContext _context;

        public UserRepo(ApartmentBookingContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            return await _context.Users.AnyAsync(u => u.Phone == phone);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User?> FindActiveUserByPhoneAsync(string phone)
        {
            User? user = await _context.Users
                .Where(u => u.Status == UserStatus.Approved)
                .Where(u => u.DeletedAt == null)
                .FirstOrDefaultAsync(u => u.Phone == phone);

            return user;
        }

        public async Task<(List<ListItemUser> Items, int Count)> GetAllWithCountAsync(UserFilterParams filterParams)
        {
            var query = _context.Users.AsNoTracking();

            if (filterParams.Status != null)
                query = query.Where(u => u.Status == filterParams.Status);

            if (filterParams.Role != null)
                query = query.Where(u => u.Role == filterParams.Role);

            if (filterParams.IsDeleted != null)
                query = filterParams.IsDeleted.Value
                    ? query.Where(u => u.DeletedAt != null)
                    : query.Where(u => u.DeletedAt == null);

            int count = await query.CountAsync();

            var items = await query
                .OrderBy(u => u.CreatedAt)
                .Select(u => new ListItemUser
                {
                    UserId = u.UserId,
                    Name = $"{u.FirstName} {u.LastName}",
                    Phone = u.Phone,
                    Role = u.Role,
                    Status = u.Status,
                    ProfilePhotoUrls = u.ProfilePhotoPath
                })
                .Skip((filterParams.Page - 1) * filterParams.PageSize)
                .Take(filterParams.PageSize)
                .ToListAsync();

            return (items, count);
        }

        public async Task UpdateUserAsync(User user)
        {
            // Already tracked
            // _context.Update(user); 

            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            User? user = await _context.Users
                .FindAsync(userId);

            return user;
        }
    }
}
