using Microsoft.EntityFrameworkCore.Migrations;

namespace HVFaceManagement.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empdataFF",
                columns: table => new
                {
                    empNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    empName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    finger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empdataFF", x => x.empNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empdataFF");
        }
    }
}
