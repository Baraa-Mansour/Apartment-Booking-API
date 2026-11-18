using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Favorite
{
    public int UserId { get; set; }

    public int ApartmentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
