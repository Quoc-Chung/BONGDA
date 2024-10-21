using System;
using System.Collections.Generic;

namespace Bay_LapTop.Models;

public partial class VwCauThuManchesterUnited
{
    public string CauLacBoId { get; set; } = null!;

    public string? TenClb { get; set; }

    public string? TenCauThu { get; set; }

    public string? TenSan { get; set; }

    public DateTime? NgayThiDau { get; set; }

    public int? ThoiDiemGhiBan { get; set; }
}
