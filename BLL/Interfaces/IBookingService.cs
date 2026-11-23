using Contracts.DTOs;
using Contracts.DTOs.booking;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        public Task CreateBookingAsync(int userId, CreateBookingRequest request);
        public Task CancelBookingAsync(int userId, int bookingId);
        public Task<(List<TenantBookingHistoryItem> Items, int Count)> GetTenantHistoryAsync(
            int tenantId,
            TenantBookingHistoryParams filterParams);
    }
}
