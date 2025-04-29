using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class Currency
{
    public int Id { get; set; }


    [StringLength(50)]
    public string Name { get; set; } = null!;


    [StringLength(50)]
    public string Symbol { get; set; } = null!;

    public double ExchangeRate { get; set; }

    public bool IsPrimary { get; set; }
}
