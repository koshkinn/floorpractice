using System;
using System.Collections.Generic;

namespace FloorMAUI;

public partial class PartnerProduct
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public int? PartnerId { get; set; }

    public int Amount { get; set; }


    public DateOnly? SellDate { get; set; }

    public virtual Partner? Partner { get; set; }

}
