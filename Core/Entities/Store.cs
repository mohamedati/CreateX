using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class Store
{
    public int Id { get; set; }


    [StringLength(100)]
    public string Name { get; set; } = null!;


    [StringLength(100)]
    public string? OwnerName { get; set; }


    [StringLength(300)]
    public string? Address { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(30)]
    public string? WhatsAppNumber { get; set; }

    public string? Link { get; set; }

    public string? AccountPhoneNumber { get; set; }

    public string? Password { get; set; }

    public string? Photo { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
