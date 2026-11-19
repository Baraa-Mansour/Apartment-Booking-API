namespace Contracts.DTOs.apartment
{
    public class ApartmentDetails : ListItemApartment
    {
        public string? Description { get; set; }
        public byte Rooms { get; set; }
        public byte Bathrooms { get; set; }
        public byte? MaxGuests { get; set; }
        public short? FloorNo { get; set; }

        public int OwnerId { get; set; }
        public string OwnerName { get; set; } = null!;
        public string OwnerPhone { get; set; } = null!;
        public string ProfilePhotoUrl { get; set; } = null!;
    }
}
