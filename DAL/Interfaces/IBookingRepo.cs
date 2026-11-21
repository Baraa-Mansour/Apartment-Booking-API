using DAL.Models;

namespace DAL.Interfaces
{
    public interface IBookingRepo
    {
        public Task<bool> IsApprovedApartmentBookingInDateAsync(int apartmentId, DateOnly startDate, DateOnly endDate);
        public Task<Booking?> GetByIdAsync(int bookingId);
        public Task AddBookingAsync(Booking booking);
        public Task UpdateBookingAsync(Booking booking);
    }
}
