using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ApartmentImage
{
    public int ImageId { get; set; }

    public int ApartmentId { get; set; }

    public string Path { get; set; } = null!;

    public bool IsCover { get; set; }

    public int SortOrder { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;
}
