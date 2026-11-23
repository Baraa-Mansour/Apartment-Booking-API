using DAL.Models;

namespace DAL.Interfaces
{
    public interface IReviewRepo
    {
        Task<Booking?> GetBookingForReviewAsync(int bookingId);
        Task AddReviewAsync(Review review);
    }
}
