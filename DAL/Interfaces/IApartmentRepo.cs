using Contracts.DTOs.apartment;

namespace DAL.Interfaces
{
    public interface IApartmentRepo
    {
        public Task<(List<ListItemApartment> Items, int Count)> GetAllWithCountAsync(ApartmentFilterParams filterParams);
    }
}
