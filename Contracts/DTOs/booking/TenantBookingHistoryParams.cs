using Contracts.Enums;

namespace Contracts.DTOs.booking
{
    public class TenantBookingHistoryParams : PaginationParams
    {
        public BookingStatus? Status { get; set; }
    }
}
