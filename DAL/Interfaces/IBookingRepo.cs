using DAL.Models;

namespace DAL.Interfaces
{
    public interface IBookingRepo
    {
        public Task<bool> IsApprovedApartmentBookingInDateAsync(int apartmentId, DateOnly startDate, DateOnly endDate);
        public Task AddBookingAsync(Booking booking);
    }
}
