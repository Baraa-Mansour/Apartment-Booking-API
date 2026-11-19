using Contracts.DTOs.apartment;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repos
{
    public class ApartmentRepo : IApartmentRepo
    {
        private readonly ApartmentBookingContext _context;
        public ApartmentRepo(ApartmentBookingContext context)
        {
            _context = context;
        }

        public async Task<(List<ListItemApartment> Items, int Count)> GetAllWithCountAsync(ApartmentFilterParams filterParams)
        {
            var query = _context.Apartments.AsNoTracking()
                .Where(a => a.DeletedAt == null)
                .Where(a => a.IsActive);

            if(filterParams.CityId != null)
                query = query.Where(a => a.CityId ==  filterParams.CityId);

            if(filterParams.GovernorateId != null)
                query = query.Where(a => a.City.GovernorateId == filterParams.GovernorateId);

            if(filterParams.PricePerNight != null)
                query = query.Where(a => a.PricePerNight <=  filterParams.PricePerNight);

            if(filterParams.Rooms != null)
                query = query.Where(a => a.Rooms >= filterParams.Rooms);

            if (filterParams.Bathrooms != null)
                query = query.Where(a => a.Bathrooms >= filterParams.Bathrooms);

            if(filterParams.AreaSqm != null)
                query = query.Where(a => a.AreaSqm >= filterParams.AreaSqm);

            if(filterParams.MaxGuests != null)
                query = query.Where(a => a.MaxGuests >=  filterParams.MaxGuests);
            
            if(filterParams.FloorNo != null)
                query = query.Where(a => a.FloorNo ==  filterParams.FloorNo);

            
            int count = await query.CountAsync();
            var items = await query
                .OrderBy(a => a.ApartmentId)
                .Select(a => new ListItemApartment
                {
                    ApartmentId = a.ApartmentId,
                    City = a.City.NameEn,
                    Governorate = a.City.Governorate.NameEn,
                    Title = a.Title,
                    Address = a.Address,
                    AreaSqm = a.AreaSqm,
                    PricePerNight = a.PricePerNight,
                    Rate = a.Reviews.Average(r => r.Rating),
                    ImagesUrls = a.ApartmentImages.Select(ai => ai.Path).ToList(),
                })
                .Skip((filterParams.Page - 1) * filterParams.PageSize)
                .Take(filterParams.PageSize)
                .ToListAsync();

            return (items,  count);
        }
    }
}
