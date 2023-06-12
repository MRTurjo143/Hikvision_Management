using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HVFaceManagement.Migrations
{
    public partial class new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceDatas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FingerData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDatas", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceDatas");
        }
    }
}
