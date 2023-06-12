using System;
using System.Collections.Generic;

#nullable disable

namespace HVFaceManagement.Models
{
    public partial class TblCatSection
    {
        public short Slno { get; set; }
        public byte ComId { get; set; }
        public short SectId { get; set; }
        public string SectName { get; set; }
        public string SectNameB { get; set; }
        public int DeptId { get; set; }
        public short? CostSect { get; set; }
        public short? CostSubSect { get; set; }
        public decimal? CostSectTotal { get; set; }
        public string Remarks { get; set; }
        public short AId { get; set; }
        public string Pcname { get; set; }
        public int? LuserId { get; set; }
        public string DeptName { get; set; }
        public byte IsIncenMark { get; set; }
        public byte IsEfficiency { get; set; }
    }
}
