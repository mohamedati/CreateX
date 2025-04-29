using Createx.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Createx.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string RefreshToken {  get; set; }

    public DateTime RefreshTokenExpiresAt { get; set; }
    public UserType UserType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
