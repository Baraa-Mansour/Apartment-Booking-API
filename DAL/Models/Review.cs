using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int BookingId { get; set; }

    public int ApartmentId { get; set; }

    public int TenantId { get; set; }

    public byte Rating { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual Booking Booking { get; set; } = null!;

    public virtual User Tenant { get; set; } = null!;
}
