using System;
using System.Collections.Generic;

namespace FloorMAUI;

public partial class Product
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }

    public string? Article { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? Coefficient { get; set; }

    public decimal? DefectPercentage { get; set; }

}
