using Contracts.DTOs.apartment;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IApartmentRepo
    {
        public Task<(List<ListItemApartment> Items, int Count)> GetAllWithCountAsync(ApartmentFilterParams filterParams);
        public Task<ApartmentDetails?> GetDetailsById(int apartmentId);
        public Task<Apartment?> GetByIdAsync(int apartmentId);
    }
}
