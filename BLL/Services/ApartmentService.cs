using BLL.Exceptions;
using BLL.Interfaces;
using Contracts.DTOs.apartment;
using DAL.Interfaces;


namespace BLL.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepo _apartmentRepo;
        public ApartmentService(IApartmentRepo apartmentRepo)
        {
            _apartmentRepo = apartmentRepo;
        }

        public async Task<(List<ListItemApartment> Items, int Count)> GetAllWithCountAsync(ApartmentFilterParams filterParams)
        {
            return await _apartmentRepo.GetAllWithCountAsync(filterParams);
        }

        public async Task<ApartmentDetails> GetDetailsById(int apartmentId)
        {
            ApartmentDetails? apartmentDetails = await _apartmentRepo.GetDetailsById(apartmentId);
            if (apartmentDetails == null)
                throw new NotFoundException("Apartment not Found");

            return apartmentDetails;
        }
    }
}
