using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class City
{
    public int Id { get; set; }


    [StringLength(50)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
