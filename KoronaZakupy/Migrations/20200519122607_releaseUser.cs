using Microsoft.EntityFrameworkCore.Migrations;

namespace KoronaZakupy.Migrations
{
    public partial class releaseUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5476af05-1033-4fef-b42c-7fd35287fb1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "551330b2-3e8d-45cf-aac6-e21628c41a90");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c690132-5a63-4f47-8f61-2755974d8f0a", "6d9a15a8-6f27-4470-ba1b-ebde0a7b743c", "Volunteer", "VOLUNTEER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35a23980-3be4-4f2b-95e5-ec3ea32d55be", "de7464cf-8051-47b0-b665-6aac36f9496d", "PersonInQuarantine", "PERSONINQUARANTINE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35a23980-3be4-4f2b-95e5-ec3ea32d55be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c690132-5a63-4f47-8f61-2755974d8f0a");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "551330b2-3e8d-45cf-aac6-e21628c41a90", "07104d42-dff0-4f1b-ad20-1b66a9d835de", "Volunteer", "VOLUNTEER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5476af05-1033-4fef-b42c-7fd35287fb1a", "09e8aa1e-6429-45c5-9755-36074db888d6", "PersonInQuarantine", "PERSONINQUARANTINE" });
        }
    }
}
