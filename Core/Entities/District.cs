using System;
using System.Collections.Generic;

namespace Createx.Core.Entities;

public partial class District
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CityId { get; set; }

    public string? Notes { get; set; }

    public virtual City City { get; set; } = null!;
    
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
