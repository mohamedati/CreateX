﻿using System;
using System.Collections.Generic;

namespace Createx.Core.Entities;

public partial class Currency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public double ExchangeRate { get; set; }

    public bool IsPrimary { get; set; }
}
