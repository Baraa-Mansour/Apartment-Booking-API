using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Governorate
{
    public int GovernorateId { get; set; }

    public string NameAr { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
