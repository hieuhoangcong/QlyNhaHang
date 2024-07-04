using System;
using System.Collections.Generic;

namespace QL_NhaHang.Models;

public partial class ItemOrder
{
    public Guid Id { get; set; }

    public Guid? IdOrder { get; set; }

    public Guid? IdMenu { get; set; }

    public int? Number { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }
}
