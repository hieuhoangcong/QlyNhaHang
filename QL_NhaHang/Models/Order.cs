using System;
using System.Collections.Generic;

namespace QL_NhaHang.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public string? CustomerName { get; set; }

    public string? NumberPhone { get; set; }

    public string? Address { get; set; }

    public DateTime? Date { get; set; }


    public Guid? IdTable { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual DinnerTable? IdTableNavigation { get; set; }

    public virtual ICollection<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
    public decimal? TotalPrice
    {
        get
        {
            return ItemOrders?.Sum(item => item.IdMenuNavigation?.Price * item.Number);
        }
    }
    public void SetCurrentDateTime()
    {
        CreateAt = DateTime.Now;
        Date = DateTime.Now;
    }


}
