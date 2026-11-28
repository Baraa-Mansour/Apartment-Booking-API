using Contracts.DTOs.booking;
using Contracts.Enums;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repos
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApartmentBookingContext _context;

        public BookingRepo(ApartmentBookingContext context)
        {
            _context = context;
        }


        public async Task AddBookingAsync(Booking booking)
        {
            await _context.AddAsync(booking);

            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetByIdAsync(int bookingId)
        {
            return await _context.Bookings
                .FindAsync(bookingId);
        }

        public async Task<(List<TenantBookingHistoryItem> Items, int Count)> GetTenantHistoryWithCountAsync(
            int tenantId,
            TenantBookingHistoryParams filterParams)
        {
            var query = _context.Bookings
                .AsNoTracking()
                .Where(b => b.TenantId == tenantId);

            if (filterParams.Status != null)
                query = query.Where(b => b.Status == filterParams.Status.Value);

            int count = await query.CountAsync();

            var items = await query
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Governorate)
                .Include(b => b.Apartment.ApartmentImages)
                .OrderByDescending(b => b.CreatedAt)
                .Skip((filterParams.Page - 1) * filterParams.PageSize)
                .Take(filterParams.PageSize)
                .Select(b => new TenantBookingHistoryItem
                {
                    BookingId = b.BookingId,
                    ApartmentId = b.ApartmentId,
                    ApartmentTitle = b.Apartment.Title,
                    Governorate = b.Apartment.City.Governorate.NameEn,
                    City = b.Apartment.City.NameEn,
                    Address = b.Apartment.Address,
                    TotalPrice = b.TotalPrice,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    Status = b.Status,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    CoverImageUrl = b.Apartment.ApartmentImages
                        .Where(i => i.IsCover)
                        .Select(i => i.Path)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return (items, count);
        }

        public async Task<bool> IsApprovedApartmentBookingInDateAsync(int apartmentId, DateOnly startDate, DateOnly endDate)
        {
            return await _context.Bookings
                .AsNoTracking()
                .AnyAsync(b =>
                    b.ApartmentId == apartmentId &&
                    b.Status == BookingStatus.Approved &&
                    b.StartDate <= endDate &&
                    b.EndDate >= startDate);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasPendingModificationRequestAsync(int bookingId)
        {
            return await _context.BookingModificationRequests
                .AsNoTracking()
                .AnyAsync(r => r.BookingId == bookingId && r.Status == BookingModificationRequestStatus.Pending);
        }

        public async Task<bool> IsApprovedApartmentBookingInDateExcludingBookingAsync(
            int apartmentId,
            int bookingId,
            DateOnly startDate,
            DateOnly endDate)
        {
            return await _context.Bookings
                .AsNoTracking()
                .AnyAsync(b =>
                    b.ApartmentId == apartmentId &&
                    b.BookingId != bookingId &&
                    b.Status == BookingStatus.Approved &&
                    b.StartDate <= endDate &&
                    b.EndDate >= startDate);
        }

        public async Task AddBookingModificationRequestAsync(BookingModificationRequest request)
        {
            await _context.BookingModificationRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
    }
}
