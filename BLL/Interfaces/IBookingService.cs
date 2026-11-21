using Contracts.DTOs.booking;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        public Task CreateBookingAsync(int userId, CreateBookingRequest request);
    }
}
