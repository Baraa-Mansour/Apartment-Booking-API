using BLL.Exceptions;
using BLL.Interfaces;
using Contracts.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System.ComponentModel.DataAnnotations;
using Contracts.Enums;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;

        public AuthService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            await ValidateRegisterRequestAsync(request);

            User user = CreateUser(request);

            await _userRepo.AddUserAsync(user);
        }
        private async Task ValidateRegisterRequestAsync(RegisterRequest request)
        {
            if (request.Role is null || request.BirthDate is null)
                throw new ValidationException("Role and birth date are required.");

            if (request.Role == UserRoles.Admin)
                throw new ForbiddenException("Only Tenant or Owner can self-register.");

            if (request.BirthDate > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ValidationException("Birth date cannot be in the future.");

            var minAllowed = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18));
            if (request.BirthDate > minAllowed)
                throw new ValidationException("You must be at least 18 years old.");

            if (await _userRepo.ExistsByPhoneAsync(request.Phone))
                throw new ConflictException("Phone number already used");
        }
        private User CreateUser(RegisterRequest request)
        {
            DateTime currentDateTime = DateTime.UtcNow;

            User user = new User()
            {
                Phone = request.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role.Value,
                Status = UserStatus.Pending,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate.Value,
                ProfilePhotoPath = request.ProfilePhotoPath,
                IdPhotoPath = request.IdPhotoPath,
                Language = request.Language ?? UserLanguage.Ar,
                Theme = request.Theme ?? UserThemes.Light,
                CreatedAt = currentDateTime,
                UpdatedAt = currentDateTime,
            };

            return user;
        }
    }
}
