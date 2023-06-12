using System;
using System.Collections.Generic;

#nullable disable

namespace HVFaceManagement.Models
{
    public partial class TblCatDesig
    {
        public int DesigId { get; set; }
        public string DesigName { get; set; }
        public string DesigNameB { get; set; }
        public int AId { get; set; }
        public Guid WId { get; set; }
        public string Pcname { get; set; }
        public int LuserId { get; set; }
        public int? Dslno { get; set; }
        public decimal? Gsmin { get; set; }
        public string Grade { get; set; }
        public string OffGrade { get; set; }
        public byte IsGsmin { get; set; }
        public string GradeB { get; set; }
        public byte ComId { get; set; }
        public decimal NightAmt { get; set; }
        public decimal AttBonus { get; set; }
    }
}
