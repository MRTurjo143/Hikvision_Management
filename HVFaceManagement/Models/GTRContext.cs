//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//#nullable disable

//namespace HVFaceManagement.Models
//{
//    public partial class GTRDBContext : DbContext
//    {
//        public GTRDBContext()
//        {
//        }

//        public GTRDBContext(DbContextOptions<GTRDBContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<HrRawDatum> HrRawData { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=36.255.68.118; Database=gtrerp_all; user id=gterpall; password=gterpall; MultipleActiveResultSets=true;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

//            modelBuilder.Entity<HrRawDatum>(entity =>
//            {
//                entity.HasKey(e => e.AId);

//                entity.ToTable("Hr_RawData");

//                entity.HasIndex(e => e.EmpId, "IX_Hr_RawData_EmpId");

//                entity.Property(e => e.AId).HasColumnName("aId");

//                entity.Property(e => e.CardNo).HasMaxLength(80);

//                entity.Property(e => e.ComId)
//                    .IsRequired()
//                    .HasMaxLength(80);

//                entity.Property(e => e.DeviceNo).HasMaxLength(15);

//                entity.Property(e => e.DeviceType).HasMaxLength(15);

//                entity.Property(e => e.Fpid)
//                    .HasMaxLength(15)
//                    .HasColumnName("FPId");

//                entity.Property(e => e.Imei).HasMaxLength(20);

//                entity.Property(e => e.InOut).HasMaxLength(10);

//                entity.Property(e => e.Latitude).HasMaxLength(20);

//                entity.Property(e => e.LocationName).HasMaxLength(200);

//                entity.Property(e => e.Longitude).HasMaxLength(20);

//                entity.Property(e => e.LuserId).HasColumnName("LUserId");

//                entity.Property(e => e.OvNmark)
//                    .HasMaxLength(15)
//                    .HasColumnName("OvNMark");

//                entity.Property(e => e.Pcname)
//                    .HasMaxLength(15)
//                    .HasColumnName("PCName");

//                entity.Property(e => e.Qrdata)
//                    .HasMaxLength(100)
//                    .HasColumnName("QRData");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
