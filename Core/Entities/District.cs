using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class District
{
    public int Id { get; set; }


    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int CityId { get; set; }


    [StringLength(1000)]
    public string? Notes { get; set; }

    public virtual City City { get; set; } = null!;
    
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
