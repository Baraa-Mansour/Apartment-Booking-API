using Contracts.DTOs.apartment;

namespace BLL.Interfaces
{
    public interface IApartmentService
    {
        public Task<(List<ListItemApartment> Items, int Count)> GetAllWithCountAsync(ApartmentFilterParams filterParams);
        public Task<ApartmentDetails> GetDetailsById(int apartmentId);
    }
}
