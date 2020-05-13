using Microsoft.EntityFrameworkCore.Migrations;

namespace KoronaZakupy.Migrations
{
    public partial class AlfaUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b22d2e42-e20e-43e6-8b6a-12fb390350c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7b24089-f5ec-4433-b073-66cf4ea00987");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "551330b2-3e8d-45cf-aac6-e21628c41a90", "07104d42-dff0-4f1b-ad20-1b66a9d835de", "Volunteer", "VOLUNTEER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5476af05-1033-4fef-b42c-7fd35287fb1a", "09e8aa1e-6429-45c5-9755-36074db888d6", "PersonInQuarantine", "PERSONINQUARANTINE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5476af05-1033-4fef-b42c-7fd35287fb1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "551330b2-3e8d-45cf-aac6-e21628c41a90");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7b24089-f5ec-4433-b073-66cf4ea00987", "0e5db044-83af-4499-b79d-8a740b8e26bb", "Volunteer", "VOLUNTEER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b22d2e42-e20e-43e6-8b6a-12fb390350c3", "afc51d0c-f75f-4ebd-8da8-e3b3b51e776c", "PersonInQuarantine", "PERSONINQUARANTINE" });
        }
    }
}
