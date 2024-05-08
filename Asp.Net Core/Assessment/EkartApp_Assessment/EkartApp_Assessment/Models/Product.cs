using System;
using System.Collections.Generic;

namespace EkartApp_Assessment.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public int? QuantityAvailable { get; set; }
}
