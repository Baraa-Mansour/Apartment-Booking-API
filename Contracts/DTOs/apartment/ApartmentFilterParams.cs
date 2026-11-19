namespace Contracts.DTOs.apartment
{
    public class ApartmentFilterParams : PaginationParams
    {
        public int? CityId { get; set; }
        public int? GovernorateId { get; set; }
        public decimal? PricePerNight { get; set; }
        public byte? Rooms {  get; set; }
        public byte? Bathrooms { get; set; }
        public int? AreaSqm { get; set; }
        public byte? MaxGuests { get; set; }
        public short? FloorNo { get; set; }
    }
}
