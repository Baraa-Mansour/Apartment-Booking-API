using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly ApartmentBookingContext _context;

        public ReviewRepo(ApartmentBookingContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetBookingForReviewAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Review)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
    }
}
