namespace Contracts.DTOs.apartment
{
    public class ApartmentFilterParams : PaginationParams
    {
        public int? CityId { get; set; }
        public int? GovernorateId { get; set; }
        public decimal? PricePerNight { get; set; }
        public int? Rooms {  get; set; }
        public int? Bathrooms { get; set; }
        public int? AreaSqm { get; set; }
        public int? MaxGuests { get; set; }
        public int? FloorNo { get; set; }
    }
}
