using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class Branch
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;


    [StringLength(50)]
    public string? ManagerName { get; set; }


    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public int CityId { get; set; }

    public int DistrictId { get; set; }


    [StringLength(100)]
    public string? Address { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int StoreId { get; set; }

    public bool IsPublish { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual District District { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
