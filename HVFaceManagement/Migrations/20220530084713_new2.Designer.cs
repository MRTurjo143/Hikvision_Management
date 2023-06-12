﻿// <auto-generated />
using System;
using HVFaceManagement.MyDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HVFaceManagement.Migrations
{
    [DbContext(typeof(MyDb))]
    [Migration("20220530084713_new2")]
    partial class new2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HVFaceManagement.Models.deviceData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CardNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("EmpImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("FingerData")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("id");

                    b.ToTable("DeviceDatas");
                });

            modelBuilder.Entity("HVFaceManagement.Models.empdata", b =>
                {
                    b.Property<string>("empNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("empName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("empNo");

                    b.ToTable("empdata");
                });

            modelBuilder.Entity("HVFaceManagement.Models.empdataFF", b =>
                {
                    b.Property<string>("empNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("empName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("finger")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("empNo");

                    b.ToTable("empdataFF");
                });
#pragma warning restore 612, 618
        }
    }
}
