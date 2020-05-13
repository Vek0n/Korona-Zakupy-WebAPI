using Microsoft.EntityFrameworkCore.Migrations;

namespace KoronaZakupy.Migrations
{
    public partial class xD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7b24089-f5ec-4433-b073-66cf4ea00987", "0e5db044-83af-4499-b79d-8a740b8e26bb", "Volunteer", "VOLUNTEER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b22d2e42-e20e-43e6-8b6a-12fb390350c3", "afc51d0c-f75f-4ebd-8da8-e3b3b51e776c", "PersonInQuarantine", "PERSONINQUARANTINE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b22d2e42-e20e-43e6-8b6a-12fb390350c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7b24089-f5ec-4433-b073-66cf4ea00987");
        }
    }
}
