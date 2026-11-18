using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Apartment
{
    public int ApartmentId { get; set; }

    public int OwnerId { get; set; }

    public int CityId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Address { get; set; } = null!;

    public decimal PricePerNight { get; set; }

    public byte Rooms { get; set; }

    public byte Bathrooms { get; set; }

    public int? AreaSqm { get; set; }

    public byte? MaxGuests { get; set; }

    public short? FloorNo { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<ApartmentImage>? ApartmentImages { get; set; } = new List<ApartmentImage>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
