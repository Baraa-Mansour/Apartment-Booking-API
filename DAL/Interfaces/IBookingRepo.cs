using Contracts.DTOs.booking;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IBookingRepo
    {
        public Task<bool> IsApprovedApartmentBookingInDateAsync(int apartmentId, DateOnly startDate, DateOnly endDate);
        public Task<Booking?> GetByIdAsync(int bookingId);
        public Task<(List<TenantBookingHistoryItem> Items, int Count)> GetTenantHistoryWithCountAsync(int tenantId, TenantBookingHistoryParams filterParams);
        public Task AddBookingAsync(Booking booking);
        public Task UpdateBookingAsync(Booking booking);
        public Task<bool> HasPendingModificationRequestAsync(int bookingId);
        public Task<bool> IsApprovedApartmentBookingInDateExcludingBookingAsync(int apartmentId, int bookingId, DateOnly startDate, DateOnly endDate);
        public Task AddBookingModificationRequestAsync(BookingModificationRequest request);
    }
}
