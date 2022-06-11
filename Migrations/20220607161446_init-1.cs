using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourierApi.Migrations.Couriers
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courrier",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TYPE_COURRIER = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    OBJET_COURRIER = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    EXPIDITEUR_COURRIER = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DESTINATAIRE_COURRIER = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DATE_COURRIER = table.Column<DateTime>(type: "date", nullable: true),
                    TAGS_COURRIER = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    COURRIER_FAVORISER = table.Column<int>(type: "int", nullable: true),
                    COURRIER_ARCHIVER = table.Column<int>(type: "int", nullable: true),
                    COURRIER_URGENT = table.Column<int>(type: "int", nullable: true),
                    ID_USER = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courrier", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courrier");
        }
    }
}
