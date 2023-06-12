using System;
using System.Collections.Generic;

#nullable disable

namespace HVFaceManagement.Models
{
    public partial class DeviceData
    {
        public int Id { get; set; }
        public string CardNo { get; set; }
        public byte[] EmpImage { get; set; }
        public byte[] FingerData { get; set; }
    }
}
