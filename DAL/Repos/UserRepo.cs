using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Contracts.Enums;


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
    }
}
