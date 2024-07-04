using System;
using System.Collections.Generic;

namespace QL_NhaHang.Models;

public partial class DinnerTable
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public int client { get; set; } 


    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
