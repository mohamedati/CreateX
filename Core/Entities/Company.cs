using System;
using System.Collections.Generic;

namespace Createx.Core.Entities;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int StoreId { get; set; }

    public string Photo { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
