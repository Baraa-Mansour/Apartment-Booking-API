using BLL.Exceptions;
using BLL.Interfaces;
using Contracts.DTOs.review;
using Contracts.Enums;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IUserRepo _userRepo;

        public ReviewService(IReviewRepo reviewRepo, IUserRepo userRepo)
        {
            _reviewRepo = reviewRepo;
            _userRepo = userRepo;
        }

        public async Task CreateReviewAsync(int tenantId, CreateReviewRequest request)
        {
            if (request == null)
                throw new InvalidOperationException("Review Request Is Required");

            ValidateCreateRequest(request);

            User? tenant = await _userRepo.GetUserByIdAsync(tenantId);
            ValidateTenantToReview(tenant);

            Booking? booking = await _reviewRepo.GetBookingForReviewAsync(request.BookingId!.Value);
            ValidateBookingToReview(booking, tenantId);

            Review review = CreateReviewObject(booking!, tenantId, request.Rating);

            await _reviewRepo.AddReviewAsync(review);
        }

        private static void ValidateCreateRequest(CreateReviewRequest request)
        {
            if (request.BookingId is null || request.BookingId <= 0)
                throw new InvalidOperationException("Booking Id Is Required");

            if (request.Rating < 1 || request.Rating > 5)
                throw new InvalidOperationException("Rating Must Be Between 1 And 5");
        }

        private static void ValidateTenantToReview(User? tenant)
        {
            if (tenant == null || tenant.DeletedAt != null)
                throw new NotFoundException("User Not Found");

            if (tenant.Status != UserStatus.Approved)
                throw new ForbiddenException("Only Approved User Can Add Review");

            if (tenant.Role != UserRoles.Tenant)
                throw new ForbiddenException("Only Tenant Can Add Review");
        }

        private static void ValidateBookingToReview(Booking? booking, int tenantId)
        {
            if (booking == null)
                throw new NotFoundException("Booking Not Found");

            if (booking.TenantId != tenantId)
                throw new ForbiddenException("You Can Review Only Your Own Booking");

            if (booking.Status != BookingStatus.Completed)
                throw new ForbiddenException("Only Completed Booking Can Be Reviewed");

            if (booking.Review != null)
                throw new ConflictException("This Booking Has Already Been Reviewed");
        }

        private static Review CreateReviewObject(Booking booking, int tenantId, byte rating)
        {
            return new Review
            {
                BookingId = booking.BookingId,
                ApartmentId = booking.ApartmentId,
                TenantId = tenantId,
                Rating = rating,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
