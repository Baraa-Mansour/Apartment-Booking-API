using BLL.Exceptions;
using BLL.Interfaces;
using Contracts.DTOs.booking;
using DAL.Interfaces;
using DAL.Models;
using System.ComponentModel.DataAnnotations;
using Contracts.Enums;

namespace BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IApartmentRepo _apartmentRepo;
        private readonly IUserRepo _userRepo;

        public BookingService(IBookingRepo bookingRepo, IApartmentRepo apartmentRepo, IUserRepo userRepo)
        {
            _bookingRepo = bookingRepo;
            _apartmentRepo = apartmentRepo;
            _userRepo = userRepo;
        }


        public async Task CreateBookingAsync(int userId, CreateBookingRequest request)
        {
            ValidateCreateRequest(request);

            User? user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User Not Found");
            ValidateUserToBook(user);

            Apartment? apartment = await  _apartmentRepo.GetByIdAsync(request.ApartmentId!.Value);
            if (apartment == null)
                throw new NotFoundException("Apartment Not Found");
            ValidateApartmentToBook(apartment!);
            await CheckApartmentAvailablityInDateAsync(apartment!.ApartmentId, request.StartDate!.Value, request.EndDate!.Value);

            Booking booking = CreateBookingObject(userId, apartment.PricePerNight, request);

            await _bookingRepo.AddBookingAsync(booking);
        }

        public async Task CancelBookingAsync(int userId, int bookingId)
        {
            Booking? booking = await _bookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
                throw new NotFoundException("Booking Not Found");
            if (booking.TenantId != userId)
                throw new ForbiddenException("Can Cancel Your Booking Only");
            ValidateBookingToCancel(booking);

            User? user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User Not Found");
            ValidateUserToCancelBook(user);
            ChangeBookingStatus(booking, BookingStatus.Cancelled);

            await _bookingRepo.UpdateBookingAsync(booking);
        }

        public async Task<(List<TenantBookingHistoryItem> Items, int Count)> GetTenantHistoryAsync(
            int tenantId,
            TenantBookingHistoryParams filterParams)
        {
            User? user = await _userRepo.GetUserByIdAsync(tenantId);
            if (user == null)
                throw new NotFoundException("User Not Found");

            ValidateTenantForHistory(user);

            return await _bookingRepo.GetTenantHistoryWithCountAsync(tenantId, filterParams);
        }

        static private void ChangeBookingStatus(Booking booking, BookingStatus status)
        {
            booking.Status = status;
            booking.UpdatedAt = DateTime.UtcNow;
        }
        static private void ValidateBookingToCancel(Booking booking)
        {
            if (booking.Status != BookingStatus.Pending && booking.Status != BookingStatus.Approved)
                throw new InvalidOperationException("Can Cancel Only Approved/Pending Booking");
            if (booking.StartDate <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidOperationException("Cant Cancel Old/Currecnt Booking");
        }
        static private Booking CreateBookingObject(int userId, decimal apartmentPricePerNight, CreateBookingRequest request)
        {
            Booking booking = new Booking();
            DateTime currentDateTime = DateTime.UtcNow;

            booking.TenantId = userId;
            booking.ApartmentId = request.ApartmentId!.Value;
            booking.StartDate = request.StartDate!.Value;
            booking.EndDate = request.EndDate!.Value;
            booking.Status = BookingStatus.Pending;
            booking.TotalPrice = apartmentPricePerNight * (booking.EndDate.DayNumber - booking.StartDate.DayNumber);
            booking.CreatedAt = currentDateTime;
            booking.UpdatedAt = currentDateTime;

            return booking;
        }
        
        static private void ValidateCreateRequest(CreateBookingRequest request)
        {
            if (request.StartDate is null || request.StartDate <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidOperationException("Start Date Should be In Future");
            if (request.EndDate is null || request.EndDate <= request.StartDate)
                throw new InvalidOperationException("End Date Should Be Greater/Equal Start Date");
        }
        
        static private void ValidateApartmentToBook(Apartment apartment)
        {
            if (apartment.DeletedAt is not null)
                throw new NotFoundException("Apartment Not Found");

            if (!apartment.IsActive)
                throw new InvalidOperationException("Apartment Not Active");
        }
        
        private async Task CheckApartmentAvailablityInDateAsync(int apartmentId, DateOnly startDate, DateOnly endDate)
        {
            if (await _bookingRepo.IsApprovedApartmentBookingInDateAsync(apartmentId, startDate, endDate))
                throw new ConflictException("Apartment Booked In This Date");
        }
        
        static private void ValidateUserToBook(User user)
        {
            if (user.DeletedAt is not null)
                throw new NotFoundException("User Not Found");
            if (user.Status != UserStatus.Approved)
                throw new ForbiddenException("Only Approved User Can Book");
            if (user.Role != UserRoles.Tenant)
                throw new ForbiddenException("Only Tenant Can Book");
        }
        static private void ValidateUserToCancelBook(User user)
        {
            if (user.DeletedAt is not null)
                throw new NotFoundException("User Not Found");
            if (user.Status != UserStatus.Approved)
                throw new ForbiddenException("Only Approved User Can Cancel");
        }
        static private void ValidateTenantForHistory(User user)
        {
            if (user.DeletedAt is not null)
                throw new NotFoundException("User Not Found");
            if (user.Status != UserStatus.Approved)
                throw new ForbiddenException("Only Approved User Can View Booking History");
            if (user.Role != UserRoles.Tenant)
                throw new ForbiddenException("Only Tenant Can View Booking History");
        }
    }
}
