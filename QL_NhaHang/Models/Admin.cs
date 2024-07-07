using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QL_NhaHang.Models;

public partial class Admin
{
    [Key]
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Passwork { get; set; }
    public string? Email { get; set; }
    
}
