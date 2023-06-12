using System;
using System.Collections.Generic;

#nullable disable

namespace HVFaceManagement.Models
{
    public partial class TblMachineNoHik
    {
        public byte ComId { get; set; }
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }
        public byte IsActive { get; set; }
        public byte IsTft { get; set; }
        public short? LuserId { get; set; }
        public string Pcname { get; set; }
        public string Status { get; set; }
        public byte IsFinger { get; set; }
        public string Hikuser { get; set; }
        public string Hikpassword { get; set; }
        public byte UnitId { get; set; }
    }
}
