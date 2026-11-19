namespace Contracts.DTOs.apartment
{
    public class ListItemApartment
    {
        public int ApartmentId { get; set; }
        public string Governorate { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? AreaSqm { get; set; }
        public decimal PricePerNight { get; set; }
        public double? Rate { get; set; }
        public List<string> ImagesUrls { get; set; } = [];
    }
}
