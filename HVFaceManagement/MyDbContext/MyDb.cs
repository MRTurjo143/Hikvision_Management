using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVFaceManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HVFaceManagement.MyDbContext
{
    public class MyDb : DbContext
    {
        public MyDb(DbContextOptions<MyDb> options) : base(options)
        {
            
        }
     
       public DbSet<TblEmpInfo> EmpInfo { get; set; }
       public DbSet<TblCatDepartment> Dept { get; set; }
       public DbSet<TblCatSection> Section { get; set; }
       public DbSet<TblCatDesig> Design { get; set; }
       public DbSet<TblCatCompany>Company { get; set; }
       public DbSet<TblMachineNoHik>Machine { get; set; }
    }
}
