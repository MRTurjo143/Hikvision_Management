using System;
using System.Collections.Generic;

#nullable disable

namespace HVFaceManagement.Models
{
    public partial class TblCatCompany
    {
        public byte ComId { get; set; }
        public string ComCode { get; set; }
        public string ComName { get; set; }
        public string ComNameB { get; set; }
        public string ComAddress { get; set; }
        public string ComAddressB { get; set; }
        public string ComAddress2 { get; set; }
        public string ComAddress2B { get; set; }
        public string ComPhone { get; set; }
        public string ComPhone2 { get; set; }
        public string ComFax { get; set; }
        public string ComEmail { get; set; }
        public string ComWeb { get; set; }
        public string ComType { get; set; }
        public string ComAlias { get; set; }
        public string ComFinYear { get; set; }
        public string Description { get; set; }
        public string ContPerson { get; set; }
        public string ContDesig { get; set; }
        public byte IsGroup { get; set; }
        public byte IsShowCurrencySymbol { get; set; }
        public byte IsShowZeroBalance { get; set; }
        public byte IsInactive { get; set; }
        public string ImgName { get; set; }
        public string ImgName2 { get; set; }
        public short? AId { get; set; }
        public Guid WId { get; set; }
        public string ComMain { get; set; }
        public byte? ComMainId { get; set; }
        public string ComShortName { get; set; }
        public string ComRefName { get; set; }
        public string ComRefNameBangla { get; set; }
        public string ComMainB { get; set; }
        public byte PfcomId { get; set; }
        public string ComShortNameB { get; set; }
        public byte[] ComLogo { get; set; }
        public byte[] ComSign { get; set; }
    }
}
