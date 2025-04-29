using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class Category
{
    public int Id { get; set; }


    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public int StoreId { get; set; }

    public string Photo { get; set; } = null!;

    public bool IsPublish { get; set; }


    [StringLength(300)]
    public string? RedirectTo { get; set; }

    public virtual Store Store { get; set; } = null!;
}
