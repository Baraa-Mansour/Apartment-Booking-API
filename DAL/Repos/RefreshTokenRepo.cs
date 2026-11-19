using DAL.Data;
using DAL.Interfaces;
using DAL.Models;


namespace DAL.Repos
{
    public class RefreshTokenRepo : IRefreshTokenRepo
    {
        private readonly ApartmentBookingContext _context;

        public RefreshTokenRepo(ApartmentBookingContext context)
        {
            _context = context;
        }

        public async Task AddRefreshToken(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }
    }
}
