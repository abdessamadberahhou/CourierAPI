using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourierApi.Migrations.Files
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    ID_FILE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_COURRIER = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FILE = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    FileExtention = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__image__2C7C2D9405D45317", x => x.ID_FILE);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "files");
        }
    }
}
