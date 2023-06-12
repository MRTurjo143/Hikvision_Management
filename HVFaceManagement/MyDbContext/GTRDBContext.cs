
//using GTERP.Models.Buffers;
using HVFaceManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
//using AlanJuden.MvcReportViewer;

namespace HVFaceManagement
{


    public partial class GTRDBContext : DbContext
    {

        public GTRDBContext()
            : base()
        {
        }

        public GTRDBContext(DbContextOptions<GTRDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<HR_Emp_DeviceInfoHIK> HR_Emp_DeviceInfoHIK { get; set; }
        public virtual DbSet<HR_MachineNoHIK> HR_MachineNoHIK { get; set; }
        public virtual DbSet<HrRawDatum> HR_RawData { get; set; }
    }
}