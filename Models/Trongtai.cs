using System;
using System.Collections.Generic;

namespace Bay_LapTop.Models;

public partial class Trongtai
{
    public string TrongTaiId { get; set; } = null!;

    public string? HoVaTen { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? QueHuong { get; set; }

    public string? QuocTich { get; set; }

    public int? SoNamKinhNghiem { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<TrongtaiTrandau> TrongtaiTrandaus { get; set; } = new List<TrongtaiTrandau>();
}
