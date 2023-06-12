using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HIKDeviceManagement.Migrations
{
    public partial class DeviceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HR_Emp_DeviceInfoHIK",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    fingerData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deviceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_Emp_DeviceInfoHIK", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HR_MachineNoHIK",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuserId = table.Column<short>(type: "smallint", nullable: true),
                    Pcname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hikuser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hikpassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inout = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_MachineNoHIK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HR_RawData",
                columns: table => new
                {
                    AId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fpid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpId = table.Column<int>(type: "int", nullable: true),
                    DtPunchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtPunchTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StNo = table.Column<int>(type: "int", nullable: true),
                    InOut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OvNmark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNew = table.Column<int>(type: "int", nullable: true),
                    Pcname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuserId = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qrdata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imei = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PicFront = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PicBack = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_RawData", x => x.AId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HR_Emp_DeviceInfoHIK");

            migrationBuilder.DropTable(
                name: "HR_MachineNoHIK");

            migrationBuilder.DropTable(
                name: "HR_RawData");
        }
    }
}
