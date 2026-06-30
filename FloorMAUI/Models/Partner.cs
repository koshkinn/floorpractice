using System;
using System.Collections.Generic;

namespace FloorMAUI;

public partial class Partner
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string Name { get; set; }

    public string CeoName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string Itn { get; set; }

    public int Rating { get; set; }
    public int TotalAmount { get; set; }
    public string GetDiscount
    {
        get
        {
            if (TotalAmount < 10000)
            {
                return "0%";
            }
            else if (TotalAmount > 10000 && TotalAmount < 50000)
            {
                return "5%";
            }
            else if (TotalAmount > 50000 && TotalAmount < 300000)
            {
                return "10%";
            }
            else return "15%";
        }
    }
    //public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();
    //seems to be a link to another entity, commented since it does not look like i should display it anywhere for now
}
