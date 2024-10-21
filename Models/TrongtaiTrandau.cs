using System;
using System.Collections.Generic;

namespace Bay_LapTop.Models;

public partial class TrongtaiTrandau
{
    public string TrongTaiId { get; set; } = null!;

    public string TranDauId { get; set; } = null!;

    public string? ViTri { get; set; }

    public virtual Trandau TranDau { get; set; } = null!;

    public virtual Trongtai TrongTai { get; set; } = null!;
}
