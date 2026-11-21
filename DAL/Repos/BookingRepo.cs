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
    }
}
