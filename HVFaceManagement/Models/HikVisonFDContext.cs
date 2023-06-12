using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HVFaceManagement.Models
{
    public partial class HikVisonFDContext : DbContext
    {
        public HikVisonFDContext()
        {
        }

        public HikVisonFDContext(DbContextOptions<HikVisonFDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeviceData> DeviceDatas { get; set; }
        public virtual DbSet<EmpdataFf> EmpdataFfs { get; set; }
        public virtual DbSet<Empdatum> Empdata { get; set; }
        public virtual DbSet<TblCatCompany> TblCatCompanies { get; set; }
        public virtual DbSet<TblCatDepartment> TblCatDepartments { get; set; }
        public virtual DbSet<TblCatDesig> TblCatDesigs { get; set; }
        public virtual DbSet<TblCatSection> TblCatSections { get; set; }
        public virtual DbSet<TblEmpInfo> TblEmpInfos { get; set; }
        public virtual DbSet<TblMachineNoHik> TblMachineNoHiks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HikVisonFD;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DeviceData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<EmpdataFf>(entity =>
            {
                entity.HasKey(e => e.EmpNo);

                entity.ToTable("empdataFF");

                entity.Property(e => e.EmpNo).HasColumnName("empNo");

                entity.Property(e => e.EmpName).HasColumnName("empName");

                entity.Property(e => e.Finger).HasColumnName("finger");

                entity.Property(e => e.Img).HasColumnName("img");
            });

            modelBuilder.Entity<Empdatum>(entity =>
            {
                entity.HasKey(e => e.EmpNo);

                entity.ToTable("empdata");

                entity.Property(e => e.EmpNo).HasColumnName("empNo");

                entity.Property(e => e.EmpName).HasColumnName("empName");

                entity.Property(e => e.Img).HasColumnName("img");
            });

            modelBuilder.Entity<TblCatCompany>(entity =>
            {
                entity.HasKey(e => e.ComId);

                entity.ToTable("tblCat_Company");

                entity.Property(e => e.ComId).HasColumnName("comId");

                entity.Property(e => e.AId).HasColumnName("aId");

                entity.Property(e => e.ComAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("comAddress");

                entity.Property(e => e.ComAddress2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("comAddress2");

                entity.Property(e => e.ComAddress2B)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("comAddress2B");

                entity.Property(e => e.ComAddressB)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("comAddressB");

                entity.Property(e => e.ComAlias)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comAlias");

                entity.Property(e => e.ComCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("comCode");

                entity.Property(e => e.ComEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comEmail");

                entity.Property(e => e.ComFax)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comFax");

                entity.Property(e => e.ComFinYear)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("comFinYear");

                entity.Property(e => e.ComLogo).HasColumnType("image");

                entity.Property(e => e.ComMain)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ComMainB)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ComMainId).HasColumnName("ComMainID");

                entity.Property(e => e.ComName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comName");

                entity.Property(e => e.ComNameB)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("comNameB");

                entity.Property(e => e.ComPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comPhone");

                entity.Property(e => e.ComPhone2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comPhone2");

                entity.Property(e => e.ComRefName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ComRefNameBangla)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ComShortName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ComShortNameB)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ComSign).HasColumnType("image");

                entity.Property(e => e.ComType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comType");

                entity.Property(e => e.ComWeb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comWeb");

                entity.Property(e => e.ContDesig)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contDesig");

                entity.Property(e => e.ContPerson)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contPerson");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ImgName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImgName2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsInactive).HasColumnName("isInactive");

                entity.Property(e => e.PfcomId).HasColumnName("PFComId");

                entity.Property(e => e.WId)
                    .HasColumnName("wId")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<TblCatDepartment>(entity =>
            {
                entity.HasKey(e => new { e.ComId, e.DeptId });

                entity.ToTable("tblCat_Department");

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.AId).HasColumnName("aId");

                entity.Property(e => e.Buid).HasColumnName("BUId");

                entity.Property(e => e.Buname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BUName");

                entity.Property(e => e.DeptBangla)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCategory)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LuserId).HasColumnName("LUserId");

                entity.Property(e => e.Pcname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PCName");

                entity.Property(e => e.Slno).HasColumnName("SLNo");
            });

            modelBuilder.Entity<TblCatDesig>(entity =>
            {
                entity.HasKey(e => new { e.DesigId, e.ComId })
                    .HasName("PK_tblCat_Desig_1");

                entity.ToTable("tblCat_Desig");

                entity.Property(e => e.AId).HasColumnName("aId");

                entity.Property(e => e.AttBonus).HasColumnType("money");

                entity.Property(e => e.DesigName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DesigNameB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dslno)
                    .HasColumnName("DSLNO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Grade)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GradeB)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gsmin)
                    .HasColumnType("smallmoney")
                    .HasColumnName("GSMin")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsGsmin).HasColumnName("isGSMin");

                entity.Property(e => e.LuserId).HasColumnName("LUserId");

                entity.Property(e => e.NightAmt).HasColumnType("smallmoney");

                entity.Property(e => e.OffGrade)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pcname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PCName");

                entity.Property(e => e.WId)
                    .HasColumnName("wId")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<TblCatSection>(entity =>
            {
                entity.HasKey(e => new { e.ComId, e.SectId })
                    .HasName("PK_tblCat_Section_2");

                entity.ToTable("tblCat_Section");

                entity.Property(e => e.AId).HasColumnName("aId");

                entity.Property(e => e.CostSectTotal).HasColumnType("money");

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LuserId).HasColumnName("LUserId");

                entity.Property(e => e.Pcname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PCName");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SectName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.SectNameB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Slno).HasColumnName("SLNo");
            });

            modelBuilder.Entity<TblEmpInfo>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("tblEmp_Info");

                entity.Property(e => e.EmpId).ValueGeneratedNever();

                entity.Property(e => e.AEmpId)
                    .HasColumnName("aEmpID")
                    .HasComment("For Serialize the employee");

                entity.Property(e => e.AId).HasColumnName("aId");

                entity.Property(e => e.AttBnsAmt).HasColumnType("money");

                entity.Property(e => e.Band)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.BandIncen)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.BankAcNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BankId).HasDefaultValueSql("((1))");

                entity.Property(e => e.BirthCertNo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroup)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Bs)
                    .HasColumnType("money")
                    .HasColumnName("BS");

                entity.Property(e => e.Bse)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("BSE");

                entity.Property(e => e.BusStop)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CadreType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CardNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CardNoOld)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CashSalary).HasColumnType("money");

                entity.Property(e => e.Caste)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cdid).HasColumnName("CDId");

                entity.Property(e => e.ComId).HasColumnName("ComID");

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.DesigFirst)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DesigId).HasColumnName("DesigID");

                entity.Property(e => e.DrivingLicenseNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DtApprove)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtApprove");

                entity.Property(e => e.DtBirth)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtBirth");

                entity.Property(e => e.DtCardAssign)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtCardAssign");

                entity.Property(e => e.DtConfirm)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtConfirm");

                entity.Property(e => e.DtDaughterBirth)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtDaughterBirth");

                entity.Property(e => e.DtDexpiry)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtDExpiry");

                entity.Property(e => e.DtDissue)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtDIssue");

                entity.Property(e => e.DtFatherBirth)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtFatherBirth");

                entity.Property(e => e.DtIncrement)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtIncrement");

                entity.Property(e => e.DtJoin)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtJoin");

                entity.Property(e => e.DtMarrige)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtMarrige");

                entity.Property(e => e.DtMotherBirth)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtMotherBirth");

                entity.Property(e => e.DtOtentitled)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtOTEntitled");

                entity.Property(e => e.DtPexpiry)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtPExpiry");

                entity.Property(e => e.DtPf)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtPF");

                entity.Property(e => e.DtPissue)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtPIssue");

                entity.Property(e => e.DtPplaceIssue)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtPPlaceIssue");

                entity.Property(e => e.DtReleased)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtReleased");

                entity.Property(e => e.DtSonBirth)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtSonBirth");

                entity.Property(e => e.DtSpouseBirth)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtSpouseBirth");

                entity.Property(e => e.DtSubmit)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtSubmit")
                    .HasDefaultValueSql("('1990-01-01')");

                entity.Property(e => e.DtTransport)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dtTransport");

                entity.Property(e => e.EduLvl)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyMob)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpBehavior)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCatagory)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCurrAdd)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCurrCity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCurrDist)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCurrDistId).HasDefaultValueSql("((1))");

                entity.Property(e => e.EmpCurrPo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmpCurrPO");

                entity.Property(e => e.EmpCurrPs)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmpCurrPS");

                entity.Property(e => e.EmpCurrVill)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCurrZip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDaughter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDaughterAdd)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDaughterEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDaughterMobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFather)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFatherAdd)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFatherB)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFatherEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFatherMobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpKnowledge)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMobile)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMother)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMotherAdd)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMotherB)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMotherEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMotherMobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmpNameB)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmpNomineeMob)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.EmpNomineeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPerAdd)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPerCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPerDist)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPerDistId).HasDefaultValueSql("((1))");

                entity.Property(e => e.EmpPerPo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmpPerPO");

                entity.Property(e => e.EmpPerPs)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmpPerPS");

                entity.Property(e => e.EmpPerVill)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPerZip)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPicLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpRefMob)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EmpRemarks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpReplace)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSonAdd)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSonEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSonMobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSpectacles)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSpouse)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSpouseAdd)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSpouseB)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSpouseEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSpouseMobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSts)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmpTinno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmpTINNo");

                entity.Property(e => e.EmpType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("Staff or Worker");

                entity.Property(e => e.EmployementType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Floor)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Fpid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("FPId");

                entity.Property(e => e.Gpamt)
                    .HasColumnType("money")
                    .HasColumnName("GPAmt");

                entity.Property(e => e.Grade)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.GradeIns)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gs)
                    .HasColumnType("money")
                    .HasColumnName("GS");

                entity.Property(e => e.Gse)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("GSE");

                entity.Property(e => e.Gsstart)
                    .HasColumnType("money")
                    .HasColumnName("GSStart");

                entity.Property(e => e.Hr)
                    .HasColumnType("smallmoney")
                    .HasColumnName("HR");

                entity.Property(e => e.IncenBns).HasColumnType("smallmoney");

                entity.Property(e => e.IncomeTax).HasColumnType("smallmoney");

                entity.Property(e => e.IncrSession)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsAcc).HasColumnName("isACC");

                entity.Property(e => e.IsAllowGp).HasColumnName("IsAllowGP");

                entity.Property(e => e.IsAllowGradebns).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsAllowOt).HasColumnName("IsAllowOT");

                entity.Property(e => e.IsAllowOtno).HasColumnName("isAllowOTNo");

                entity.Property(e => e.IsAllowOtstaff).HasColumnName("IsAllowOTStaff");

                entity.Property(e => e.IsAllowPf).HasColumnName("IsAllowPF");

                entity.Property(e => e.IsAllowPp).HasColumnName("IsAllowPP");

                entity.Property(e => e.IsAllowShortfall).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDisabled).HasColumnName("isDisabled");

                entity.Property(e => e.IsDormitoryFacility).HasColumnName("isDormitoryFacility");

                entity.Property(e => e.IsFwvehicle).HasColumnName("isFWVehicle");

                entity.Property(e => e.IsNid).HasColumnName("IsNID");

                entity.Property(e => e.IsNonSmv).HasColumnName("IsNonSMV");

                entity.Property(e => e.IsOffDayOt).HasColumnName("isOffDayOT");

                entity.Property(e => e.IsTrnDeduction).HasComment("0");

                entity.Property(e => e.IsTwvehicle).HasColumnName("isTWVehicle");

                entity.Property(e => e.Kpitype)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("KPIType");

                entity.Property(e => e.Line)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.LitigationDetails)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LuserId).HasColumnName("LUserId");

                entity.Property(e => e.Ma)
                    .HasColumnType("smallmoney")
                    .HasColumnName("MA");

                entity.Property(e => e.MaritalSts)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MobileAllow)
                    .HasColumnType("smallmoney")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OffDayAllowAmt).HasColumnType("smallmoney");

                entity.Property(e => e.OfficeGrade)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.OldEmpId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("OldEmpID");

                entity.Property(e => e.Operation)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.OtallowAmt)
                    .HasColumnType("smallmoney")
                    .HasColumnName("OTAllowAmt");

                entity.Property(e => e.OtherAllow).HasColumnType("money");

                entity.Property(e => e.OtherAllowE)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PassportNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PayMode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PaySource)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Pcname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PCName");

                entity.Property(e => e.PolicyNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrdBn).HasColumnType("smallmoney");

                entity.Property(e => e.PrevGross)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RegNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Religion)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SalId)
                    .HasColumnName("salId")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sex)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SexB)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftAllow).HasColumnType("smallmoney");

                entity.Property(e => e.ShiftCat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Shift Category");

                entity.Property(e => e.ShiftIdR).HasDefaultValueSql("((0))");

                entity.Property(e => e.ShiftType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SkillGradePoint)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubBandIncen)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SubSectId).HasColumnName("SubSectID");

                entity.Property(e => e.Team)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Trn).HasColumnType("smallmoney");

                entity.Property(e => e.Ts)
                    .HasColumnType("money")
                    .HasColumnName("TS");

                entity.Property(e => e.VehicleDetails)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VoterNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WId)
                    .HasColumnName("wId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.WeekDayId)
                    .HasColumnName("WeekDayID")
                    .HasDefaultValueSql("((6))");

                entity.Property(e => e.WorkPlace)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkTypeB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zone)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMachineNoHik>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblMachineNo_HIK");

                entity.Property(e => e.Hikpassword)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("HIKPassword");

                entity.Property(e => e.Hikuser)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("HIKUser");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsTft).HasColumnName("isTFT");

                entity.Property(e => e.Location)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LuserId).HasColumnName("LUserId");

                entity.Property(e => e.Pcname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PCName");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Disconnect')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
