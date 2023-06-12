using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HVFaceManagement.Models
{
    public class HrRawDatum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AId { get; set; }
        public string ComId { get; set; }
        public string DeviceNo { get; set; }
        public string CardNo { get; set; }
        public string Fpid { get; set; }
        public int? EmpId { get; set; }
        public DateTime DtPunchDate { get; set; }
        public DateTime DtPunchTime { get; set; }
        public int? StNo { get; set; }
        public string InOut { get; set; }
        public string OvNmark { get; set; }
        public int? IsNew { get; set; }
        public string Pcname { get; set; }
        public string DeviceType { get; set; }
        public int? LuserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Qrdata { get; set; }
        public string Imei { get; set; }
        public string LocationName { get; set; }
        public byte[] PicFront { get; set; }
        public byte[] PicBack { get; set; }
    }
}
