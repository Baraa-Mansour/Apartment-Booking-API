using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int ApartmentId { get; set; }

    public int TenantId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual ICollection<BookingModificationRequest> BookingModificationRequests { get; set; } = new List<BookingModificationRequest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Review? Review { get; set; }

    public virtual User Tenant { get; set; } = null!;
}
