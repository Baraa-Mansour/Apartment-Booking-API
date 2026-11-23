using Contracts.Enums;

namespace Contracts.DTOs.booking
{
    public class TenantBookingHistoryItem
    {
        public int BookingId { get; set; }
        public int ApartmentId { get; set; }
        public string ApartmentTitle { get; set; } = null!;
        public string Governorate { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CoverImageUrl { get; set; }
    }
}
