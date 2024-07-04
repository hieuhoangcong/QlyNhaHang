using System;
using System.Collections.Generic;

namespace QL_NhaHang.Models;

public partial class Menu
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Img { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
}
