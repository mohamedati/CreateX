using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class Company
{
    public int Id { get; set; }


    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int StoreId { get; set; }

    public Store Store {  get; set; }
    public byte[] Photo { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
