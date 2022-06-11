using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourierApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    first_name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    last_name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    birth_day = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    cin = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    password = table.Column<string>(type: "varchar(1500)", unicode: false, maxLength: 1500, nullable: true),
                    AVATAR = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NUM_TELE = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    IS_ADMIN = table.Column<int>(type: "int", nullable: false),
                    is_accepted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
