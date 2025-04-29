using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserId { get; set; } = null!;


    [StringLength(300)]
    public string DeliveryAddress { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    
}
