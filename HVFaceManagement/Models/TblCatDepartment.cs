using System;
using System.Collections.Generic;

#nullable disable

namespace HVFaceManagement.Models
{
    public partial class TblCatDepartment
    {
        public byte ComId { get; set; }
        public int DeptId { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public int AId { get; set; }
        public string Pcname { get; set; }
        public int? LuserId { get; set; }
        public string DeptBangla { get; set; }
        public short? Slno { get; set; }
        public int? ParentId { get; set; }
        public short? Buid { get; set; }
        public string Buname { get; set; }
        public string DeptCategory { get; set; }
    }
}
