using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class ProductType
{
    public int Id { get; set; }


    [StringLength(100)]
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
